"use client";

import { createContext, Dispatch, SetStateAction, useContext, useState } from "react";

type HeaderProviderProps = {
    children: React.ReactNode;
};

type HeaderContextData = {
    searchValue: string;
    setSearchValue: Dispatch<SetStateAction<string>>;
    placeholderSearch: string;
    setPlaceholderSearch: Dispatch<SetStateAction<string>>;
    hiddenSearch: boolean;
    setHiddenSearch: Dispatch<SetStateAction<boolean>>;
    keyItemTab: string;
    setKeyItemTab: Dispatch<SetStateAction<string>>;
    configureHeader: (config: { 
        placeholder?: string; 
        hidden?: boolean; 
        tabKey?: string; 
    }) => void;
};

export const HeaderContext = createContext<HeaderContextData | null>(null);

export const useHeader = () => {
    const context = useContext(HeaderContext);
    if (!context) throw new Error("useHeader must be used within a HeaderProvider");
    return context;
};

export default function HeaderProvider({ children }: HeaderProviderProps) {
    const [searchValue, setSearchValue] = useState<string>("");
    const [hiddenSearch, setHiddenSearch] = useState<boolean>(true);
    const [keyItemTab, setKeyItemTab] = useState<string>("");
    const [placeholderSearch, setPlaceholderSearch] = useState<string>("");

    const configureHeader = ({ placeholder, hidden, tabKey }: { 
        placeholder?: string; 
        hidden?: boolean; 
        tabKey?: string; 
    }) => {
        if (placeholder !== undefined) setPlaceholderSearch(placeholder);
        if (hidden !== undefined) setHiddenSearch(hidden);
        if (tabKey !== undefined) setKeyItemTab(tabKey);
    };

    return (
        <HeaderContext.Provider
            value={{
                searchValue,
                setSearchValue,
                hiddenSearch,
                setHiddenSearch,
                keyItemTab,
                setKeyItemTab,
                placeholderSearch,
                setPlaceholderSearch,
                configureHeader,
            }}
        >
            {children}
        </HeaderContext.Provider>
    );
}
