import { useHeader } from "@/app/context/headerContext";
import { Flex, Tabs, TabsProps } from "antd";
import React, { useEffect } from "react";

type TabsHeaderProps = {
    items?: TabsProps["items"];
    defaultActiveKey?: string;
};

export default function TabsHeader({ items, defaultActiveKey }: TabsHeaderProps) {
    const { setKeyItemTab } = useHeader();

    useEffect(() => {
        if (defaultActiveKey) {
            setKeyItemTab(defaultActiveKey);
        }
    }, [defaultActiveKey, setKeyItemTab]);

    return (
        <Flex align="center">
            <Tabs
                defaultActiveKey={defaultActiveKey}
                styles={{ header: { margin: 0 } }}
                onChange={(key) => setKeyItemTab(key)}
                items={items}
            />
        </Flex>
    );
}
