import React, { useState, useEffect } from 'react';
import { useUser } from '../components/UserProvider';
import { CreateConnection } from '../components/CreateGame';
import './Account.css';
import { HubConnection, HubConnectionState } from '@microsoft/signalr';

// Local type definition for CardSetVersion
export enum CardSetVersion {
    V2000 = 'V2000'
}

export const ACCOUNT_ROUTE = '/';

export function Account() {
    const { user, SetUserContext, ConnectionStatusMessage, SetUserContextHistory } = useUser();
    const [username, setUsername] = useState('');
    const [draftId, setDraftId] = useState('');
    const [cardSetVersion, setCardSetVersion] = useState<CardSetVersion>(CardSetVersion.V2000);
    const [status, setStatus] = useState('');
    const [error, setError] = useState('');
    const [isCreatingGroup, setIsCreatingGroup] = useState(true);
    const [isConnecting, setIsConnecting] = useState(false);

    useEffect(() => {
        setStatus(ConnectionStatusMessage());
    }, [user, status]);

    const handleCreateGroup = async () => {
        if (!username || !draftId) {
            setError('Please enter a username and draft ID');
            return;
        }

        setError('');
        setIsConnecting(true);
        try {
            const connection = await CreateConnection(username, draftId, '');
            
            // Wait for connection to be fully established
            await new Promise((resolve, reject) => {
                const interval = setInterval(() => {
                    if (connection.state === HubConnectionState.Connected) {
                        clearInterval(interval);
                        resolve(true);
                    } else if (connection.state === HubConnectionState.Disconnected) {
                        clearInterval(interval);
                        reject(new Error('Connection failed to establish'));
                    }
                }, 100);
            });

            await connection.invoke("CreateDraft", draftId, cardSetVersion);
            
            connection.on("DraftStateUpdated", (draft: any) => {
                console.log("Draft state updated:", draft);
            });

            SetUserContext({
                username: username,
                draftId: draftId,
                history: [],
                connection: connection
            });
            setStatus('Successfully created and joined draft!');
        } catch (err) {
            setError(`Failed to create draft: ${err instanceof Error ? err.message : 'Unknown error'}`);
            console.error('Create draft error:', err);
        } finally {
            setIsConnecting(false);
        }
    };

    const handleJoinGroup = async () => {
        if (!username || !draftId) {
            setError('Please enter a username and draft ID');
            return;
        }

        setError('');
        setIsConnecting(true);
        try {
            const connection = await CreateConnection(username, draftId, '');
            
            // Wait for connection to be fully established
            await new Promise((resolve, reject) => {
                const interval = setInterval(() => {
                    if (connection.state === HubConnectionState.Connected) {
                        clearInterval(interval);
                        resolve(true);
                    } else if (connection.state === HubConnectionState.Disconnected) {
                        clearInterval(interval);
                        reject(new Error('Connection failed to establish'));
                    }
                }, 100);
            });

            await connection.invoke("JoinDraft", draftId, username);
            
            connection.on("DraftStateUpdated", (draft: any) => {
                console.log("Draft state updated:", draft);
            });

            SetUserContext({
                username: username,
                draftId: draftId,
                history: [],
                connection: connection
            });
            setStatus('Successfully joined draft!');
        } catch (err) {
            setError(`Failed to join draft: ${err instanceof Error ? err.message : 'Unknown error'}`);
            console.error('Join draft error:', err);
        } finally {
            setIsConnecting(false);
        }
    };

    const handleCardSetChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setCardSetVersion(e.target.value as CardSetVersion);
    };

    return (
        <div className="App">
            <img src="mlb.png" className="App-logo" alt="logo" />
            <br />
            <div className="account-container">
                <div className="account-switch">
                    <button 
                        onClick={() => setIsCreatingGroup(true)}
                        className={isCreatingGroup ? 'active' : ''}
                    >
                        Create Group
                    </button>
                    <button 
                        onClick={() => setIsCreatingGroup(false)}
                        className={!isCreatingGroup ? 'active' : ''}
                    >
                        Join Group
                    </button>
                </div>

                <div className="account-form">
                    <div className="form-group">
                        <label>Username:</label>
                        <input 
                            type="text" 
                            value={username} 
                            onChange={(e) => setUsername(e.target.value)}
                            placeholder="Enter your username"
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label>Draft ID:</label>
                        <input 
                            type="text" 
                            value={draftId} 
                            onChange={(e) => setDraftId(e.target.value)}
                            placeholder="Enter draft ID"
                            required
                        />
                    </div>
                    {isCreatingGroup && (
                        <div className="form-group">
                            <label>Card Set Version:</label>
                            <select value={cardSetVersion} onChange={handleCardSetChange} required>
                                <option value={CardSetVersion.V2000}>2000</option>
                                {/* Add more card set versions as needed */}
                            </select>
                        </div>
                    )}
                    
                    <button 
                        onClick={isCreatingGroup ? handleCreateGroup : handleJoinGroup}
                        disabled={isConnecting || !username || !draftId || (isCreatingGroup && !cardSetVersion)}
                    >
                        {isConnecting ? 'Connecting...' : isCreatingGroup ? 'Create Group' : 'Join Group'}
                    </button>
                </div>

                {error && (
                    <div className="error-message">
                        {error}
                    </div>
                )}
                {status && (
                    <div className="status-message">
                        {status}
                    </div>
                )}
            </div>
        </div>
    );
}