import React, {useState, useEffect } from 'react';
import { useUser } from '../components/UserProvider';
import {AddressInUse} from "../config/ServerConfig";
import { CreateConnection, SendConnectionMessage } from '../components/CreateGame';
import './Account.css';
import { HubConnection } from '@microsoft/signalr';

function Account() {
    const {user, SetUserContext} = useUser();

    //TODO: username, groupid, and password input

    async function CreateAccountConnection()  {
      const connection: HubConnection = await CreateConnection("user12345", "group12345", "pass12345");
      
      SetUserContext({
        username: user.username,
        connection: connection
      })
    };

    async function DeleteAccountConnection() {
      //TODO: Delete group
    }

    function SendMsgAccountConnection() : void  {
      // TODO: check state is CONNECTED, console.log(user.connection?.state)
      if (user.connection == null){console.log("SendMsg: No Connection Found"); return;}
      SendConnectionMessage(user.connection, "user12345", "group12345", "My message here");
    };

    return(
        <div className="App">
          <img src="mlb.png" className="App-logo" alt="logo" />
          <p>
            Testing Sockets Here
          </p>
          <button onClick = {CreateAccountConnection}>
            Create Socket
          </button>
          <button onClick = {SendMsgAccountConnection}>
            Send Message
          </button>
        </div>
    )
}

export default Account;