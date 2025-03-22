import React, {useState, useEffect } from 'react';
import { useUser } from '../components/UserProvider';
import { CreateConnection, SendConnectionMessage, ReceiveConnectionMessages } from '../components/CreateGame';
import './Account.css';
import { HubConnection } from '@microsoft/signalr';

export const ACCOUNT_ROUTE = '/';

export function Account() {
    const {user, SetUserContext, ConnectionStatusMessage, SetUserContextHistory} = useUser();
    const [username, setusername] = useState('user1234');
    const [groupname, setgroupname] = useState('group1234');
    const [grouppassword, setgrouppassword] = useState('pass1234');
    const [history, sethistory] = useState([]);
    const [status, setstatus] = useState('');
    const [counter, setcounter] = useState(0);
    
    useEffect(() => {
      setstatus(ConnectionStatusMessage());
      }, [user, status]);

      async function CreateAccountConnection() {
        const connection: HubConnection = await CreateConnection(username, groupname, grouppassword);
      
      //ReceiveConnectionMessages(connection);
      connection.on("MessageReceived", (username: string, message: string) => {
          console.log("Received Message Back");
          console.log("User?" + user.username);
      });
      
      SetUserContext({
        username: username,
        groupname: groupname,
        history: [],
        connection: connection
      })
    };

    async function DeleteAccountConnection() {
      //TODO: Delete group
    }

    function SendMsgAccountConnection() : void  {
      if (user.connection == null){console.log("SendMsg: No Connection Found"); return;}
      if (user.connection.state != "Connected"){console.log("Connection Issue: Please Reconnect")}
      SendConnectionMessage(user.connection, username, groupname, `My ${counter} message here`);
      setcounter(counter + 1);
    };

    return(
        <div className="App">
          <img src="mlb.png" className="App-logo" alt="logo" />
          <br></br>
          <div className = "account-input-container">
            <label>Username: </label>
            <input className = "account-input" value={username} 
            onChange={e => setusername(e.target.value)}></input>
            <label>Groupname: </label>
            <input className = "account-input" value={groupname} 
            onChange={e => setgroupname(e.target.value)}></input>
            <label>Group Password: </label>
            <input className = "account-input" type = "password" value={grouppassword} 
            onChange={e => setgrouppassword(e.target.value)}></input>
            <button className = "account-submit" onClick = {CreateAccountConnection}>
            Join Group
            </button>
          </div>
          <div>
            <p>{status}</p>
          </div>          
        </div>
    )
}