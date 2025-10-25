import { Flex, Tabs, TabsProps } from "antd";

type TabsHeaderProps = {
    items: TabsProps["items"];
    activeKey: string;
    setActiveTab: (value: string) => void;
};

export function TabsHeader({ activeKey, items, setActiveTab }: TabsHeaderProps) {
    return (
        <Flex align="center">
            <Tabs
                styles={{ header: { margin: 0 } }}
                onChange={(key) => setActiveTab(key)}
                activeKey={activeKey}
                items={items}
            />
        </Flex>
    );
}
