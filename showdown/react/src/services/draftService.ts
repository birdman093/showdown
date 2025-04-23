import * as signalR from '@microsoft/signalr';
import { DraftState, IPlayer } from '../types/draft';

export class DraftService {
    private connection: signalR.HubConnection;
    private draftStateCallback?: (state: DraftState) => void;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${process.env.REACT_APP_BACKEND_URL}/drafthub`)
            .withAutomaticReconnect()
            .build();

        this.connection.on('DraftStateUpdated', (state: DraftState) => {
            if (this.draftStateCallback) {
                this.draftStateCallback(state);
            }
        });

        this.connection.start().catch(err => console.error('SignalR Connection Error: ', err));
    }

    public onDraftStateUpdated(callback: (state: DraftState) => void) {
        this.draftStateCallback = callback;
    }

    public async createDraft(draftId: string): Promise<void> {
        await this.connection.invoke('CreateDraft', draftId);
    }

    public async joinDraft(draftId: string, userId: string): Promise<void> {
        await this.connection.invoke('JoinDraft', draftId, userId);
    }

    public async selectPlayer(draftId: string, userId: string, playerId: string): Promise<void> {
        await this.connection.invoke('SelectPlayer', draftId, userId, playerId);
    }

    public async leaveDraft(draftId: string): Promise<void> {
        await this.connection.invoke('LeaveDraft', draftId);
    }

    // REST API calls
    public async getDraftState(draftId: string): Promise<DraftState> {
        const response = await fetch(`https://localhost:7240/Draft/${draftId}`);
        if (!response.ok) throw new Error('Failed to get draft state');
        return response.json();
    }

    public async getAvailablePlayers(draftId: string): Promise<IPlayer[]> {
        const response = await fetch(`https://localhost:7240/Draft/${draftId}/available-players`);
        if (!response.ok) throw new Error('Failed to get available players');
        return response.json();
    }

    public async getUserSelections(draftId: string, userId: string): Promise<IPlayer[]> {
        const response = await fetch(`https://localhost:7240/Draft/${draftId}/user-selections/${userId}`);
        if (!response.ok) throw new Error('Failed to get user selections');
        return response.json();
    }
}
