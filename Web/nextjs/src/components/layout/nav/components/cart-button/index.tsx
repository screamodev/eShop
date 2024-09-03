import CartDropdown from "../cart-dropdown"
import {useCart} from "../../../../../context/cart/CartContext";

export default function CartButton() {
  const { cart } = useCart();

  return <CartDropdown cart={cart} />
}
