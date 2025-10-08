"use client";

import { useHeader } from "../components/app/header";
import { Button } from "antd";

export default function App() {
    const { setNavBar } = useHeader();
    return (
        <div className="flex justify-center items-center h-full">
            body
            <Button onClick={setNavBar}>Clique aqui</Button>
        </div>
    );
}
