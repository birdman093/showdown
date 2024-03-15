import * as signalR from "@microsoft/signalr";
import { AddressInUse } from "../config/ServerConfig";
import {User, useUser} from './UserProvider'

// Global Variable here -- i.e state?
// Grab user Context with sending it to CreateConnection?

export async function CreateConnection(username: string, groupname: string, grouppassword: string) {
    const connection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${AddressInUse}/hub`)
    .withAutomaticReconnect()
    .build();

    await connection.start().catch((err) => console.log(err));

    await connection.send("JoinGroup", username, groupname, grouppassword)
        .then(() => (console.log("Joined Group")));

    return connection;
}

export async function SendConnectionMessage
(connection: signalR.HubConnection, username: string, groupname: string, message: string){
    await connection.send("NewMessage", username, groupname, message);
    console.log("Message Sent");
}

export function ReceiveConnectionMessages(connection: signalR.HubConnection){
    connection.on("MessageReceived", (username: string, message: string) => {
        // NEED TO LOG THIS!
        console.log("Message Received: " + message);
    });
}