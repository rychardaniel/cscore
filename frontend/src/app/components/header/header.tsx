"use client";

import { Flex, Input } from "antd";
import { Icon } from "@iconify/react";
import { Search } from "../header/search";
import { useHeader } from "@/app/context/headerContext";
import { Notification } from "./notification";

type HeaderProps = {
    // children: React.ReactNode;
};

export function Header({}: HeaderProps) {
    const {searchValue, setSearchValue} = useHeader()
    return (
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
                        <Icon icon="iconoir:graduation-cap" className="text-2xl text-(--blue)" />
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
                        <Search
                            value={searchValue}
                            setValue={setSearchValue}
                            placeholder="Buscar campeonato..."
                        />
                    </div>
                </Flex>
                <Notification />
                <div>User</div>
            </div>
        </Flex>
    );
}
