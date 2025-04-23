import React, { createContext, useContext, useState } from 'react';
import { HubConnection } from '@microsoft/signalr';

// Define and export User interface
export interface User {
    username: string;
    draftId: string;
    history: any[];
    connection: HubConnection | null;
}

interface UserContextType {
    user: User;
    SetUserContext: (user: User) => void;
    ConnectionStatusMessage: () => string;
    SetUserContextHistory: (history: any[]) => void;
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export function UserProvider({ children }: { children: React.ReactNode }) {
    const [user, setUser] = useState<User>({
        username: '',
        draftId: '',
        history: [],
        connection: null
    });

    const SetUserContext = (userData: User) => {
        setUser(userData);
    };

    const ConnectionStatusMessage = () => {
        return user.connection ? 'Connected' : 'Disconnected';
    };

    const SetUserContextHistory = (history: any[]) => {
        setUser(prev => ({ ...prev, history }));
    };

    return (
        <UserContext.Provider value={{ user, SetUserContext, ConnectionStatusMessage, SetUserContextHistory }}>
            {children}
        </UserContext.Provider>
    );
}

export function useUser() {
    const context = useContext(UserContext);
    if (context === undefined) {
        throw new Error('useUser must be used within a UserProvider');
    }
    return context;
}
