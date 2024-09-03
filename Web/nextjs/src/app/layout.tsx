import "./globals.css";
import { Montserrat } from 'next/font/google';
import { Metadata } from "next";
import React from "react";
import Nav from "../components/layout/nav";
import Main from "../components/layout/main";
import Footer from "../components/layout/footer";
import { getServerSession } from "next-auth/next";
import SessionProvider from "../components/providers/SessionProvider";
import {authOptions} from "../pages/api/auth/[...nextauth]";
import {cookies} from "next/headers";
import {CartProvider} from "../context/cart/CartContext";

const inter = Montserrat({
    subsets: ["latin"],
    weight: ["400"]
});

export const metadata: Metadata = {
    title: "Eshop",
    description: "Eshop - online store",
};

export default async function RootLayout({ children }: Readonly<{ children: React.ReactNode }>) {
    const session = await getServerSession(authOptions);

    return (
        <html lang="en" className={inter.className}>
        <body>
        <SessionProvider session={session}>
            <CartProvider>
            <Nav />
            <Main>{children}</Main>
            <Footer />
            </CartProvider>
        </SessionProvider>
        </body>
        </html>
    );
}
