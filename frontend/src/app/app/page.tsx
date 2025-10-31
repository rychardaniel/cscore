"use client";

import { RightOutlined } from "@ant-design/icons";
import { Button, Card, Col, Flex, Row, Typography } from "antd";
import { useRouter } from "next/navigation";

export default function App() {
    const router = useRouter();

    return (
        <Flex flex={1} vertical gap={4}>
            <Row gutter={[9, 9]}>
                <Col xs={24} sm={24} md={12} lg={12} xl={12}>
                    <Card>
                        <Flex justify="center">
                            <Typography.Title level={4}>Bem-vindo ao Cscore!</Typography.Title>
                        </Flex>
                    </Card>
                </Col>
                <Col xs={24} sm={24} md={12} lg={12} xl={12}>
                    <Card>
                        <Flex justify="center">
                            <Button
                                type="link"
                                icon={<RightOutlined color="#FFFFFF" />}
                                iconPlacement="end"
                                onClick={() => router.push("/app/championships")}
                            >
                                Ir para campeonatos
                            </Button>
                        </Flex>
                    </Card>
                </Col>
            </Row>
        </Flex>
    );
}
