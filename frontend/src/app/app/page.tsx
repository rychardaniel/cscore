"use client";

import { Button, Flex } from "antd";
import { useRouter } from "next/navigation";

export default function App() {
    const router = useRouter();

    return (
        <Flex>
            <Button type="primary" onClick={() => router.push("/app/championships")}>
                Ir para campeonatos
            </Button>
        </Flex>
    );
}
