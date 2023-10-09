import React, { createContext, useContext, useState, ReactNode } from 'react';

export type User = {
    username: string;
    connection: signalR.HubConnection | null;
};

type UserContextType = {
    user: User;
    SetUserContext: (user: User) => void;
};

type UserProviderChildren = {
    children: ReactNode;
};

const UserContext = createContext<UserContextType | undefined>(undefined);

export function UserProvider({ children } : UserProviderChildren) {
    const [user, setUser] = useState<User>({
        username: '',
        connection: null
    });

    const SetUserContext = (user: User) => {
        if (user && user.connection != null) 
        {   setUser( {
                username: user.username,
                connection: user.connection
            });
        } else 
        {   setUser({
                username: '',
                connection: null
            });
        }
    };

    // Provide the user data and the setter function to the components
    return (
        <UserContext.Provider value={{ user, SetUserContext}}>
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
