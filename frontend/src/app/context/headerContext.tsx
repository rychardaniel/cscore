"use client";

import { createContext, Dispatch, SetStateAction, useContext, useState } from "react";

type HeaderProviderProps = {
    children: React.ReactNode;
};

type HeaderContextData = {
    searchValue: string;
    setSearchValue: Dispatch<SetStateAction<string>>;
};

export const HeaderContext = createContext<HeaderContextData | null>(null);

export const useHeader = () => {
    const context = useContext(HeaderContext);
    if (!context) throw new Error("useHeader must be used within a HeaderProvider");
    return context;
};

export default function HeaderProvider({ children }: HeaderProviderProps) {
    const [searchValue, setSearchValue] = useState<string>("");

    return (
        <HeaderContext.Provider
            value={{
                searchValue,
                setSearchValue,
            }}
        >
            {children}
        </HeaderContext.Provider>
    );
}
