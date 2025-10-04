import ApplicationLayout from "@/components/applicationLayout";

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <ApplicationLayout>
            {children}
        </ApplicationLayout>
    );
}
