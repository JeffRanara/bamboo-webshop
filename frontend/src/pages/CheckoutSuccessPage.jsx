import { Link, useSearchParams } from "react-router-dom";
import { useEffect, useState, useRef } from "react";

export default function CheckoutSuccessPage() {
  const [searchParams] = useSearchParams();
  const sessionId = searchParams.get("session_id");

  const [order, setOrder] = useState(null);
  const [loading, setLoading] = useState(true);

  const hasFetched = useRef(false); // prevent double call (StrictMode)

  useEffect(() => {
    if (!sessionId) return;
    if (hasFetched.current) return;

    hasFetched.current = true;

    const fetchOrder = async () => {
      try {
        const res = await fetch(
          `https://localhost:7235/api/checkout/order-from-session/${sessionId}`
        );

        if (!res.ok) {
          console.error("Failed to fetch order");
          setLoading(false);
          return;
        }

        const data = await res.json();
        setOrder(data);
      } catch (err) {
        console.error("Error fetching order:", err);
      } finally {
        setLoading(false);
      }
    };

    fetchOrder();
  }, [sessionId]);

  return (
    <main className="min-h-screen bg-emerald-950 text-emerald-50 px-6 py-12">
      <section className="max-w-3xl mx-auto bg-emerald-900/70 border border-emerald-700 rounded-2xl p-8 shadow-lg">
        <h1 className="text-3xl font-bold mb-4">Payment successful</h1>

        <p className="text-lg mb-4">
          Thank you — your bamboo order has been received and payment was confirmed.
        </p>

        {/* Loading state */}
        {loading && (
          <p className="text-emerald-300 mb-4">Loading order details...</p>
        )}

        {/* Order details */}
        {!loading && order && (
          <div className="mb-6 bg-emerald-800/50 p-4 rounded-lg border border-emerald-700">
            <h2 className="text-xl font-semibold mb-2">Order Details</h2>

            <p><strong>Order Number:</strong> {order.orderNumber}</p>
            <p><strong>Total:</strong> £{order.totalIncVat}</p>
            <p><strong>Status:</strong> {order.paymentStatus}</p>
            <p><strong>Created:</strong> {new Date(order.createdAt).toLocaleString()}</p>
          </div>
        )}

        {/* Debug session id */}
        {sessionId && (
          <p className="text-sm text-emerald-200 break-all mb-6">
            Stripe session: {sessionId}
          </p>
        )}

        <div className="flex gap-4">
          <Link
            to="/"
            className="bg-emerald-500 text-emerald-950 px-5 py-3 rounded-lg font-semibold hover:bg-emerald-400"
          >
            Back to forest
          </Link>

          <Link
            to="/cart"
            className="border border-emerald-500 px-5 py-3 rounded-lg font-semibold hover:bg-emerald-800"
          >
            View cart
          </Link>
        </div>
      </section>
    </main>
  );
}