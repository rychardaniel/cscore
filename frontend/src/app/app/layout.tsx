import HeaderProvider from "../context/headerContext";

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <HeaderProvider>
            <div className="h-screen">{children}</div>
        </HeaderProvider>
    );
}
