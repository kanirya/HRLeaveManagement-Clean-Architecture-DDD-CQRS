// AuthContext.tsx
import React, { createContext, useState, useContext, ReactNode } from "react";
import {Client,type LoginUserDto} from "../../../services/base/ServiceClient.ts";

type AuthContextType = {
    user: string | null;
    login: (email: string, password: string) => Promise<void>;
    logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);


export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const AuthContext=new Client();
    const [user, setUser] = useState<string | null>(null);

    const login = async (email: string, password: string) => {
        try {
            const dto: LoginUserDto = { email, password };
            await AuthContext.login(dto);

            // For production: API should return a token -> save in localStorage
            localStorage.setItem("token", "dummy-jwt-token");
            setUser(email);
        } catch (err) {
            console.error("Login failed", err);
            throw err;
        }
    };

    const logout = () => {
        localStorage.removeItem("token");
        setUser(null);
    };

    return (
        <AuthContext.Provider value={{ user, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used within AuthProvider");
    return ctx;
};
