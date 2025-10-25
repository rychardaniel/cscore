"use client";

import { Flex, TabsProps } from "antd";
import { Icon } from "@iconify/react";
import { Search } from "../header/search";
import { useHeader } from "@/app/context/headerContext";
import { Notification } from "./notification";
import { AvatarIcon } from "./avatar";
import { TabsHeader } from "./tabs";
import { usePathname, useRouter } from "next/navigation";
import { useEffect, useMemo, useState } from "react";

import { TABS_CONFIG } from "./constants";

export function Header() {
    const { searchValue, setSearchValue } = useHeader();
    const [activeTab, setActiveTab] = useState<string>("");
    const [hiddenSearch, setHiddenSearch] = useState<boolean>(false);
    const [placeholderSearh, setPlaceholderSearch] = useState<string>("");
    const pathname = usePathname();
    const router = useRouter();

    const tabs: TabsProps["items"] = useMemo(() => {
        return TABS_CONFIG.map(({ key, label }) => ({ key, label }));
    }, []);

    useEffect(() => {
        const currentTab = TABS_CONFIG.find((tab) => pathname === tab.path);
        if (currentTab && currentTab.key !== activeTab) {
            setActiveTab(currentTab.key);
            setHiddenSearch(currentTab.searchHidden);
            setPlaceholderSearch(currentTab.placeholder);
        }
    }, [pathname]);

    const handleTabChange = (key: string) => {
        const tab = TABS_CONFIG.find((t) => t.key === key);
        if (tab) {
            setActiveTab(key);
            setHiddenSearch(tab.searchHidden);
            setPlaceholderSearch(tab.placeholder);

            router.push(tab.path);
        }
    };

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
                    <TabsHeader items={tabs} activeKey={activeTab} setActiveTab={handleTabChange} />
                    <Flex style={{ width: "20%" }}>
                        <Flex hidden={hiddenSearch} style={{ width: "100%" }}>
                            <Search
                                value={searchValue}
                                setValue={setSearchValue}
                                placeholder={placeholderSearh}
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
