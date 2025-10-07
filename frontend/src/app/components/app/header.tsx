import { Flex } from "antd";
import { Icon } from "@iconify/react";

type Props = {};

export default function Header({}: Props) {
    return (
        <Flex
            justify="center"
            align="center"
            style={{ height: 65, borderBottom: "2px solid #ddd" }}
        >
            <div className="w-full max-w-[1200px] p-4 h-full flex justify-between items-center">
                <Flex gap={5}>
                    <Icon icon="iconoir:graduation-cap" className="text-2xl text-(--blue)" />
                    <h2 className="font-bold">Cscore</h2>
                </Flex>
                <div>Cscore</div>
            </div>
        </Flex>
    );
}
