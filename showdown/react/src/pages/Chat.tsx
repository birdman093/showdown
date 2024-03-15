import React, {useState, useEffect } from 'react';
import { useUser } from '../components/UserProvider';
import { SendConnectionMessage } from '../components/CreateGame';
import './Chat.css';
import { HubConnection } from '@microsoft/signalr';

function Chat() {
    const {user, SetUserContext, ConnectionStatusMessage, SetUserContextHistory} = useUser();
    const [status, setstatus] = useState('');
    const [counter, setcounter] = useState(0);
    const [history, sethistory] = useState<string[]>([]);
    
    useEffect(() => {
      setstatus(ConnectionStatusMessage());
      sethistory(user.history);
      }, [user, status, counter]);

    async function SendMsgAccountConnection()  {
      if (user.connection == null){console.log("SendMsg: No Connection Found"); return;}
      if (user.connection.state != "Connected"){console.log("Connection Issue: Please Reconnect")}
      let newmsg = `My ${counter} message here`;
      await SendConnectionMessage(user.connection, user.username, user.groupname, newmsg);
      SetUserContextHistory(newmsg);
      setcounter(counter + 1);     
    };

    function MsgHistory() : JSX.Element {
      return <div>
        {history.map(
          (msg, idx) => (<div id={"message"+idx.toString()} key={idx}>{msg}</div>)
        )}
      </div>
    }

    return(
        <div className="App">
          <div>
            <p>{status}</p>
          </div> 
          <h1>Message History</h1>
          <div className = "msg-container">
            <MsgHistory/>
          </div>
          <button onClick = {SendMsgAccountConnection}>Send Message</button>     
        </div>
    )
}

export default Chat;