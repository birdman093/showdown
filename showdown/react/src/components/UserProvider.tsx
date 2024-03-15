import React, { createContext, useContext, useState, ReactNode } from 'react';

export type User = {
    username: string;
    groupname: string;
    history: string[];
    connection: signalR.HubConnection | null;
};

type UserContextType = {
    user: User;
    SetUserContext: (user: User) => void;
    ConnectionStatusMessage: () => string;
    SetUserContextHistory: (message: string) => void;
};

type UserProviderChildren = {
    children: ReactNode;
};

const UserContext = createContext<UserContextType | undefined>(undefined);

export function UserProvider({ children } : UserProviderChildren) {
    const [user, setUser] = useState<User>({
        username: '',
        groupname: '',
        history: [],
        connection: null
    });

    const ConnectionStatusMessage = () => {
        if (user.connection && user.connection.state == "Connected"){
            return `${user.username} connected to ${user.groupname}`
        } else {
            return `No connection`
        }
    };

    const SetUserContext = (user: User) => {
        if (user && user.connection != null) 
        {   setUser(user);
        } else 
        {   setUser({
                username: user.username,
                groupname: user.groupname,
                history: [],
                connection: null
            });
        }
    };

    const SetUserContextHistory = (message: string) => {
        if (user && user.connection != null) 
        {   setUser({
                username: user.username,
                groupname: user.groupname,
                history: [...user.history, `${user.username}: ${message}`],
                connection: user.connection
            });
        }
    };

    return (
        <UserContext.Provider value={{ user, SetUserContext, 
        ConnectionStatusMessage, SetUserContextHistory}}>
            {children}
        </UserContext.Provider>
    );
}

export function useUser() {
    const context = useContext(UserContext);
    if (!context) {
        throw new Error("useUser must be used within a UserProvider");
    }
    return context;
}
