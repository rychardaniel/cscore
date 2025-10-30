type ContentLayoutProps = {
    children: React.ReactNode;
};

export function ContentLayout({ children }: ContentLayoutProps) {
    return <div className="w-full max-w-[900px] p-4 h-full">{children}</div>;
}
