"use client";

import { Header } from "../components/header/header";
import { useHeader } from "../context/headerContext";

export default function App() {
    const { searchValue } = useHeader();
    return (
        <>
            <Header />
            <div className="h-[calc(100vh-4rem)]">
                <div className="flex justify-center items-center h-full">
                    <h1>{searchValue}</h1>
                </div>
            </div>
        </>
    );
}
