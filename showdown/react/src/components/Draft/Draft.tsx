import React, { useEffect, useState } from 'react';
import { DraftState, IPlayer } from '../../types/draft';
import { DraftService } from '../../services/draftService';

interface DraftProps {
    draftId: string;
    userId: string;
}

const Draft: React.FC<DraftProps> = ({ draftId, userId }) => {
    const [draftState, setDraftState] = useState<DraftState | null>(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [draftService] = useState(() => new DraftService());

    useEffect(() => {
        // Join draft when component mounts
        draftService.joinDraft(draftId, userId);

        // Subscribe to draft updates
        draftService.onDraftStateUpdated((state) => {
            setDraftState(state);
        });

        // Initial state load
        draftService.getDraftState(draftId)
            .then(setDraftState)
            .catch(console.error);

        // Cleanup
        return () => {
            draftService.leaveDraft(draftId);
        };
    }, [draftId, userId]);

    const filteredPlayers = draftState?.availablePlayers.filter(player => 
        player.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
        player.team.toLowerCase().includes(searchTerm.toLowerCase())
    ) || [];

    const handleSelectPlayer = (player: IPlayer) => {
        if (draftState?.currentTurn !== userId) {
            alert("It's not your turn!");
            return;
        }
        draftService.selectPlayer(draftId, userId, player.name);
    };

    if (!draftState) return <div>Loading...</div>;

    return (
        <div className="draft-container">
            <div className="draft-header">
                <h2>MLB Showdown Draft</h2>
                <div className="draft-status">
                    <p>Round: {draftState.currentRound}</p>
                    <p>Current Turn: {draftState.currentTurn === userId ? 'Your Turn!' : draftState.currentTurn}</p>
                </div>
            </div>

            <div className="draft-content">
                <div className="available-players">
                    <h3>Available Players</h3>
                    <input
                        type="text"
                        placeholder="Search players..."
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                    <div className="players-list">
                        {filteredPlayers.map((player) => (
                            <div 
                                key={player.name} 
                                className="player-card"
                                onClick={() => handleSelectPlayer(player)}
                            >
                                <h4>{player.name}</h4>
                                <p>{player.team}</p>
                                {/* Add more player details as needed */}
                            </div>
                        ))}
                    </div>
                </div>

                <div className="team-selections">
                    <h3>Team Selections</h3>
                    {Object.entries(draftState.userSelections).map(([teamId, players]) => (
                        <div key={teamId} className="team-section">
                            <h4>{teamId === userId ? 'Your Team' : `Team ${teamId}`}</h4>
                            <div className="team-players">
                                {players.map((player) => (
                                    <div key={player.name} className="player-card selected">
                                        <h4>{player.name}</h4>
                                        <p>{player.team}</p>
                                    </div>
                                ))}
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default Draft;
