"use client";

import { Button, Popover, Transition } from "@headlessui/react";
import { Fragment, useState } from "react";
import Link from "next/link";
import Image from "next/image";
import {CartItem} from "../../../../../config/types/cart/cartItem";

type CartDropdownProps = {
  cart: CartItem[];
};

const CartDropdown = ({ cart }: CartDropdownProps) => {
  const [cartDropdownOpen, setCartDropdownOpen] = useState(false);

  const open = () => setCartDropdownOpen(true);
  const close = () => setCartDropdownOpen(false);

  const totalItems = cart.reduce((total, item) => total + item.count, 0) || 0;

  const openAndCancel = () => {
    open();
  };

  return (
      <div
          className="h-full z-50"
          onMouseEnter={openAndCancel}
          onMouseLeave={close}
      >
        <Popover className="relative h-full">
          <Popover.Button className="h-full">
            <Link
                className="hover:text-ui-fg-base"
                href="/cart"
            >{`Cart (${totalItems})`}</Link>
          </Popover.Button>
          <Transition
              show={cartDropdownOpen}
              as={Fragment}
              enter="transition ease-out duration-200"
              enterFrom="opacity-0 translate-y-1"
              enterTo="opacity-100 translate-y-0"
              leave="transition ease-in duration-150"
              leaveFrom="opacity-100 translate-y-0"
              leaveTo="opacity-0 translate-y-1"
          >
            <Popover.Panel
                static
                className="hidden small:block absolute top-[calc(100%+1px)] right-0 bg-white border-x border-b border-gray-200 w-[420px] text-ui-fg-base shadow-lg"
            >
              <div className="p-4 flex items-center justify-between border-b border-gray-200">
                <h3 className="text-lg font-semibold">Cart</h3>
              </div>
              {cart && cart.length ? (
                  <div className="p-4">
                    {cart.map((item) => (
                        <div key={item.id} className="flex items-center py-2 border-b last:border-b-0">
                          <div className="w-16 h-16 relative overflow-hidden rounded-md">
                            <Image src={item.pictureUrl} alt={item.name} layout="fill" objectFit="cover" />
                          </div>
                          <div className="ml-4 flex-1">
                            <h4 className="text-sm font-medium">{item.name}</h4>
                            <p className="text-xs text-gray-500">{`Size: ${item.size}`}</p>
                            <p className="text-sm">{`$${item.price} x ${item.count}`}</p>
                          </div>
                          <div className="text-right">
                            <p className="text-sm font-semibold">{`$${item.price * item.count}`}</p>
                          </div>
                        </div>
                    ))}
                    <div className="mt-4">
                      <Link href="/cart" passHref>
                        <Button className="w-full py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600">
                          Go to Cart
                        </Button>
                      </Link>
                    </div>
                  </div>
              ) : (
                  <div className="p-4 flex flex-col items-center justify-center">
                    <div className="bg-gray-900 text-white flex items-center justify-center w-6 h-6 rounded-full text-sm">
                      0
                    </div>
                    <p className="mt-2 text-sm text-gray-500">Your shopping bag is empty.</p>
                    <Link href="/" passHref>
                      <Button className="mt-4 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600">
                        Explore Products
                      </Button>
                    </Link>
                  </div>
              )}
            </Popover.Panel>
          </Transition>
        </Popover>
      </div>
  );
};

export default CartDropdown;
