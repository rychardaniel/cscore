"use client";

import { Avatar, Flex, Input, TabsProps } from "antd";
import { Icon } from "@iconify/react";
import { Search } from "../header/search";
import { useHeader } from "@/app/context/headerContext";
import { Notification } from "./notification";
import { AvatarIcon } from "./avatar";
import TabsHeader from "./tabs";

type HeaderProps = {
    itemsTabs?: TabsProps["items"];
    defaultActiveKey?: string;
};

export function Header({ itemsTabs, defaultActiveKey }: HeaderProps) {
    const { searchValue, setSearchValue, hiddenSearch } = useHeader();
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
                    <TabsHeader items={itemsTabs} defaultActiveKey={defaultActiveKey} />
                    <Flex style={{ width: "20%" }}>
                        <Flex hidden={hiddenSearch} style={{ width: "100%" }}>
                            <Search
                                value={searchValue}
                                setValue={setSearchValue}
                            />
                        </Flex>
                    </Flex>
                </Flex>
                <Notification />
                <AvatarIcon />
            </div>
        </Flex>
    );
}
