import type React from "react"
import type { Metadata } from "next"
import { GeistSans } from "geist/font/sans"
import { GeistMono } from "geist/font/mono"
import { Analytics } from "@vercel/analytics/next"
import { Playfair_Display } from "next/font/google"
import { Suspense } from "react"

import {Link, Outlet} from "react-router-dom";
function Layout() {
    return (
        <>
            <div className="Header sticky z-50 top-0">
                <h2 className="text-gray-100">Leave Management</h2>
                <Link className="Button rounded-xl bg-gray-100 px-2 py-1 hover:bg-light-200 " to="/login">
                    Login
                </Link>
            </div>
        <Outlet />


        </>

    )
}

export default Layout

const playfair = Playfair_Display({ subsets: ["latin"], variable: "--font-playfair", display: "swap" })

export const metadata: Metadata = {
    title: "v0 App",
    description: "Created with v0",
    generator: "v0.app",
}

export default function RootLayout({
                                       children,
                                   }: Readonly<{
    children: React.ReactNode
}>) {
    return (
        <html lang="en">
        <body className={`font-sans ${GeistSans.variable} ${GeistMono.variable} ${playfair.variable}`}>
        <Suspense fallback={null}>{children}</Suspense>
        <Analytics />
        </body>
        </html>
    )
}
