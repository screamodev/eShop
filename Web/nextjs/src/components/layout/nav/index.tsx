"use client"

import { Suspense } from "react"
import CartButton from "./components/cart-button"
import Link from "next/link";
import { signIn, signOut, useSession } from "next-auth/react";
import {useRouter} from "next/navigation";

export default function Nav() {
  const { data: session } = useSession();
  const router = useRouter();

  const handleSubmit = async () => {
    router.push("http://www.alevelwebsite.com:5003/connect/endsession");
    await signOut({redirect: false})
  }

  return (
    <div className="sticky top-0 inset-x-0 z-50 group">
      <header className="relative h-16 mx-auto border-b duration-200 bg-white border-ui-border-base">
        <nav className="content-container txt-xsmall-plus text-ui-fg-subtle flex items-center justify-between w-full h-full text-small-regular">
          <div className="flex items-center h-full">
            <Link
              href="/"
              className="txt-compact-xlarge-plus hover:text-ui-fg-base uppercase"
              data-testid="nav-store-link"
            >
              eShop
            </Link>
          </div>

          <div className="flex items-center gap-x-6 h-full flex-1 basis-0 justify-end">
            <div className="small:flex items-center gap-x-6 h-full">
              {!session ? <a href="/api/auth/signin" onClick={() => signIn("identity-server4")}
                                  className="hover:text-ui-fg-base"
              >
                Login
              </a> : <a href="http://www.alevelwebsite.com:5003/connect/endsesion" onClick={handleSubmit}
                                  className="hover:text-ui-fg-base"
              >
                Logout
              </a>}
            </div>
            {session && <Suspense
                fallback={
                  <Link
                      className="hover:text-ui-fg-base flex gap-2"
                      href="/cart"
                >
                  Cart
                </Link>
              }
            >
              <CartButton />
              <Link
                  className="hover:text-ui-fg-base flex gap-2"
                  href="/orders"
              >
                Orders
              </Link>
            </Suspense>}
          </div>
        </nav>
      </header>
    </div>
  )
}
