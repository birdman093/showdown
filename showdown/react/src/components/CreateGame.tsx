import * as signalR from "@microsoft/signalr";
import { AddressInUse } from "../config/ServerConfig";
import { User } from './UserProvider';

export async function CreateConnection(username: string, draftId: string, password: string): Promise<signalR.HubConnection> {
    const connection: signalR.HubConnection = new signalR.HubConnectionBuilder()
        .withUrl(`${AddressInUse}/hub`, {
            transport: signalR.HttpTransportType.WebSockets
        })
        .withAutomaticReconnect()
        .build();

    try {
        await connection.start();
        
        // Wait for the connection to be fully established
        await new Promise((resolve, reject) => {
            const interval = setInterval(() => {
                if (connection.state === signalR.HubConnectionState.Connected) {
                    clearInterval(interval);
                    resolve(true);
                } else if (connection.state === signalR.HubConnectionState.Disconnected) {
                    clearInterval(interval);
                    reject(new Error('Connection failed to establish'));
                }
            }, 100);
        });

        console.log("Connection established");
        await connection.invoke("JoinDraft", draftId, username);
        console.log("Joined Draft");
        return connection;
    } catch (err) {
        console.error("Failed to connect:", err);
        throw new Error(`Connection error: ${err instanceof Error ? err.message : 'Unknown error'}`);
    }
}

export async function SendConnectionMessage(connection: signalR.HubConnection, username: string, draftId: string, message: string): Promise<void> {
    if (connection.state !== signalR.HubConnectionState.Connected) {
        throw new Error("Connection is not in Connected state");
    }
    await connection.invoke("SendMessage", username, draftId, message);
}

export async function ReceiveConnectionMessages(connection: signalR.HubConnection): Promise<void> {
    connection.on("MessageReceived", (username: string, message: string) => {
        console.log("Received Message:", username, message);
    });
}