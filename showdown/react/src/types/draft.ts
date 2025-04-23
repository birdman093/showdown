export interface IGameCard {
    // Add game card properties as needed
}

export interface IPlayer {
    name: string;
    team: string;
    cardSetVersion: string;
    gameCard: IGameCard;
    selected: boolean;
}

export interface DraftState {
    draftId: string;
    connectedUsers: string[];
    userSelections: { [userId: string]: IPlayer[] };
    availablePlayers: IPlayer[];
    currentTurn: string;
    currentRound: number;
    isActive: boolean;
}
