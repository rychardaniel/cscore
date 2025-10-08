"use client";

import { Flex, Input } from "antd";
import { Icon } from "@iconify/react";
import { createContext, useContext } from "react";

type HeaderProviderProps = {
    children: React.ReactNode;
};

type HeaderContextData = {
    setNavBar: () => void;
};

export const HeaderContext = createContext<HeaderContextData | null>(null);

export const useHeader = () => {
    const context = useContext(HeaderContext);
    if (!context) throw new Error("useHeader must be used within a HeaderProvider");

    return context;
};

export default function HeaderProvider({ children }: HeaderProviderProps) {
    function setNavBar() {
        console.log("Rodou");
    }

    return (
        <HeaderContext.Provider value={{ setNavBar }}>
            <Flex
                justify="center"
                align="center"
                style={{
                    height: 64,
                    borderBottom: "1px solid var(--gray-light-2)",
                    boxShadow: "0 1px 2px 0 rgba(0, 0, 0, 0.05)",
                }}
            >
                <div className="w-full max-w-[1200px] p-4 h-full flex justify-between items-center gap-4">
                    <Flex justify="space-between" style={{ width: "100%" }} align="center">
                        <Flex gap={5}>
                            <Icon
                                icon="iconoir:graduation-cap"
                                className="text-2xl text-(--blue)"
                            />
                            <h2 className="font-bold">Cscore</h2>
                        </Flex>
                        <nav>
                            <ul className="flex gap-6">
                                <li>Home</li>
                                <li>About</li>
                                <li>Contact</li>
                                <li>Login</li>
                            </ul>
                        </nav>
                        <div className="w-2/10">
                            <Input placeholder="Search TODO" />
                        </div>
                    </Flex>
                    <div>User information</div>
                </div>
            </Flex>
            <div className="h-[calc(100vh-4rem)]">{children}</div>
        </HeaderContext.Provider>
    );
}
