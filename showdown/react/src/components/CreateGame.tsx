import * as signalR from "@microsoft/signalr";
import { AddressInUse } from "../config/ServerConfig";
import {User, useUser} from './UserProvider'

// Global Variable here -- i.e state?
// Grab user Context with sending it to CreateConnection?

export async function CreateConnection(username: string, groupname: string, grouppassword: string) {
    const connection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${AddressInUse}/hub`)
    //.withAutomaticReconnect()
    .build();

    connection.on("MessageReceived", (username: string, message: string) => {
        console.log("Received Message Back")
    });

    await connection.start().catch((err) => console.log(err));

    await connection.send("JoinGroup", username, groupname, grouppassword)
        .then(() => (console.log("Join Group has been Sent")));

    return connection;
}

export async function SendConnectionMessage
(connection: signalR.HubConnection, username: string, groupname: string, message: string){
    await connection.send("NewMessage", username, groupname, message)
        .then(() => (console.log("Message has been Sent")));
}