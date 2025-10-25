import { Header } from "../components/header/header";
import { LayoutDefault } from "../components/layout/layoutDefault";
import HeaderProvider from "../context/headerContext";

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
