import { Layout } from "antd"

type ApplicationLayoutProps = {
    children: React.ReactNode
}

export default function ApplicationLayout({ children }: ApplicationLayoutProps) {
  return (
    <Layout style={{ minHeight: "100vh", backgroundColor: "transparent" }}>
      <h1>Application Layout</h1>
      {children}
    </Layout>
  )
}