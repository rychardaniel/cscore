import { Metadata } from "next";
import { Header } from "../components/header/header";
import { LayoutDefault } from "../components/layout/layoutDefault";
import HeaderProvider from "../context/headerContext";

export const metadata: Metadata = {
    title: "Cscore",
    description: "Application for live academic games",
    icons: "/favicon.svg",
};

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <HeaderProvider>
            <LayoutDefault header={<Header />} content={children} />
        </HeaderProvider>
    );
}
