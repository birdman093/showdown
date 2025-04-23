import React, { useState, useEffect } from 'react';
import { useUser } from '../components/UserProvider';
import { SendConnectionMessage } from '../components/CreateGame';
import { HubConnection } from '@microsoft/signalr';
import './Chat.css';

export const CHAT_ROUTE = '/chat';

export function Chat() {
    const { user, SetUserContext, ConnectionStatusMessage } = useUser();
    const [counter, setcounter] = useState(0);
    const [status, setstatus] = useState('');

    useEffect(() => {
        setstatus(ConnectionStatusMessage());
    }, [user, status]);

    async function SendMsgAccountConnection() {
        if (user.connection === null) {
            console.log("SendMsg: No Connection Found");
            return;
        }
        if (user.connection.state !== "Connected") {
            console.log("Connection Issue: Please Reconnect");
            return;
        }
        let newmsg = `My ${counter} message here`;
        await SendConnectionMessage(user.connection, user.username, user.draftId, newmsg);
        SetUserContext({
            ...user,
            history: [...user.history, newmsg]
        });
        setcounter(counter + 1);
    }

    return (
        <div className="App">
            <div className="chat-container">
                <div className="chat-header">
                    <h2>Chat</h2>
                    <p>Status: {status}</p>
                </div>
                <div className="chat-messages">
                    {user.history.map((msg, index) => (
                        <div key={index} className="chat-message">
                            {msg}
                        </div>
                    ))}
                </div>
                <button onClick={SendMsgAccountConnection}>
                    Send Message
                </button>
            </div>
        </div>
    );
}