const getSessionId = () => {
  let id = localStorage.getItem('sessionId');
  if (!id) {
    id = crypto.randomUUID();
    localStorage.setItem('sessionId', id);
  }
  return id;
};

import { useEffect, useMemo, useRef, useState } from 'react';
import './App.css';
import { Routes, Route, Link, useSearchParams } from 'react-router-dom';

const speciesImages = {
  'Phyllostachys vivax Huanwenzhu': '/images/vivax-huanwenzhu.jpg',
  'Phyllostachys vivax McClure': '/images/vivax-mcclure.jpg',
  'Phyllostachys vivax Aureocaulis': '/images/vivax-aureocaulis.jpg',
  'Phyllostachys parvifolia': '/images/parvifolia.jpg',
  'Phyllostachys atrovaginata': '/images/atrovaginata.jpg',
  'Phyllostachys aureosulcata': '/images/aureosulcata-green.jpg',
  'Phyllostachys aureosulcata Aureocaulis': '/images/aureosulcata-aureocaulis.jpg',
  'Phyllostachys aureosulcata Spectabilis': '/images/aureosulcata-spectabilis.jpg',
  'Phyllostachys aureosulcata Argus': '/images/aureosulcata-argus.jpg',
  'Phyllostachys nigra': '/images/nigra-black.jpg',
  'Phyllostachys nigra Henonis': '/images/nigra-henonis.jpg',
  'Phyllostachys nigra Punctata': '/images/nigra-punctata.jpg',
  'Phyllostachys bissetii': '/images/bissetii.jpg',
  'Phyllostachys decora': '/images/decora.jpg',
  'Phyllostachys nuda': '/images/nuda.jpg',
  'Sasa kurilensis': '/images/sasa-kurilensis.jpg',
  'Bashania qingchengshanensis': '/images/bashania-qing.jpg',
  'Semiarundinaria fastuosa': '/images/semiarundinaria-fastuosa.jpg',
  'Fargesia nitida x murieliae Obelisk': '/images/fargesia-obelisk.jpg',
  'Fargesia murieliae Red Zebra': '/images/fargesia-red-zebra.jpg',
  'Fargesia nitida x murieliae Schensbossen': '/images/fargesia-schensbossen.jpg',
  'Fargesia sp. Rufa': '/images/fargesia-rufa.jpg',
  'Fargesia robusta Campbell': '/images/fargesia-campbell.jpg',
  'Fargesia nitida Jürgen': '/images/fargesia-jurgen.jpg',
  'Fargesia sp. Jiuzhaigou 1': '/images/fargesia-jiuzhaigou-1.jpg',
  'Fargesia sp. Jiuzhaigou Deep Purple': '/images/fargesia-deep-purple.jpg',
  'Fargesia demissa Gerry': '/images/fargesia-gerry.jpg',
  'Fargesia denudata Xian 2': '/images/fargesia-xian-2.jpg',
  'Fargesia sp. KR5287': '/images/fargesia-kr5287.jpg',
};


function getSpeciesKey(item) {
  return [item.genus, item.species, item.cultivar].filter(Boolean).join(' ');
}

function getSpeciesImage(item) {
  return speciesImages[getSpeciesKey(item)] || '/images/parvifolia.jpg';
}

function getPrimaryUse(item) {
  return item.technicalProfile?.primaryUses || item.notes || 'Ornamental / living material';
}

function rectanglesOverlap(a, b, gap = 18) {
  return !(
    a.right + gap < b.left ||
    a.left > b.right + gap ||
    a.bottom + gap < b.top ||
    a.top > b.bottom + gap
  );
}

function calculateForestLayout(items, maxHeight, maxDiameter, viewportWidth) {
  const fieldWidth = Math.max(900, viewportWidth);
  const cardHeightBase = 120;
  const cardHeightScale = 500;
  const cardWidthBase = 54;
  const cardWidthScale = 76;
  const captionHeight = 58;

  const heroRect = {
    left: fieldWidth * 0.05,
    top: window.innerHeight * 0.06,
    right: fieldWidth * 0.05 + 285,
    bottom: window.innerHeight * 0.06 + 450,
  };

  const lanes = [0.88, 0.79, 0.70, 0.61, 0.52, 0.43, 0.34, 0.25, 0.16]
    .map((ratio) => ratio * fieldWidth);

  const placed = [];
  const result = new Map();

  items.forEach((item, index) => {
    const heightRatio = (item.heightHigh || 1) / maxHeight;
    const widthRatio = (item.diameterHigh || 1) / maxDiameter;

    const width = cardWidthBase + cardWidthScale * widthRatio;
    const height = cardHeightBase + cardHeightScale * heightRatio + captionHeight;

    const isClumping = item.habit === 1 || item.genus === 'Fargesia';
    const biologicalClusterBias = isClumping ? 0.5 : -0.5;

    const preferredLaneIndex = Math.max(
      0,
      Math.min(
        lanes.length - 1,
        Math.floor((1 - heightRatio) * (lanes.length - 1)) + biologicalClusterBias
      )
    );

    let best = null;

    lanes.forEach((laneX, laneIndex) => {
      let y = 10 + ((index * 29 + laneIndex * 17) % 68) - heightRatio * 22;

      while (y < 2600) {
        const organicX = (((index * 37 + laneIndex * 11) % 25) - 12) * 1.7;
        const sunPull = heightRatio * 14;
        const x = laneX + organicX + sunPull;

        const rect = {
          left: x - width / 2,
          right: x + width / 2,
          top: y,
          bottom: y + height,
        };

        const collides =
          rectanglesOverlap(rect, heroRect, 22) ||
          placed.some((existing) => rectanglesOverlap(rect, existing, 12));

        if (!collides) {
          const laneDistance = Math.abs(laneIndex - preferredLaneIndex);

          const score =
            y * 1.0 +
            laneDistance * 55 -
            heightRatio * (lanes.length - laneIndex) * 4;

          if (!best || score < best.score) {
            best = { x, y, rect, score };
          }

          break;
        }

        const stepNoise = ((index * 13 + laneIndex * 7) % 13) - 6;
        y += 11 + heightRatio * 7 + stepNoise;
      }
    });

    if (!best) {
      best = {
        x: lanes[lanes.length - 1],
        y: placed.length * 80,
        rect: {
          left: lanes[lanes.length - 1] - width / 2,
          right: lanes[lanes.length - 1] + width / 2,
          top: placed.length * 80,
          bottom: placed.length * 80 + height,
        },
      };
    }

    placed.push(best.rect);
    result.set(item.id, {
      x: best.x,
      y: best.y,
    });
  });

  const forestHeight = Math.max(
    window.innerHeight,
    ...placed.map((rect) => rect.bottom + 80)
  );

  return { positions: result, forestHeight };
}

function CheckoutSuccessPage() {
  const [searchParams] = useSearchParams();
  const sessionId = searchParams.get('session_id');

  const [order, setOrder] = useState(null);
  const [loadingOrder, setLoadingOrder] = useState(true);
  const [orderError, setOrderError] = useState(null);

  const hasRun = useRef(false);

  useEffect(() => {
    if (hasRun.current) return;
    hasRun.current = true;

    const runSuccessPageActions = async () => {
      try {
        if (sessionId) {
          const orderResponse = await fetch(
            `https://localhost:7235/api/checkout/order-from-session/${sessionId}`
          );

          if (!orderResponse.ok) {
            throw new Error(`Order fetch failed: ${orderResponse.status}`);
          }

            let orderData = await orderResponse.json();

            if (orderData.paymentStatus === 0) {
              await new Promise((resolve) => setTimeout(resolve, 1500));

              const retryResponse = await fetch(
                `https://localhost:7235/api/checkout/order-from-session/${sessionId}`
              );

              if (retryResponse.ok) {
                orderData = await retryResponse.json();
              }
            }

            setOrder(orderData);
        }

        const sessionIdToClear = localStorage.getItem("sessionId");

        await fetch("https://localhost:7235/api/cart/clear", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            sessionId: sessionIdToClear
          })
        });
      } catch (err) {
        console.error("Success page error:", err);
        setOrderError("Could not load order details.");
      } finally {
        setLoadingOrder(false);
      }
    };

    runSuccessPageActions();
  }, [sessionId]);

  return (
    <main className="site-shell">
      <section className="hero-box" style={{ maxWidth: '720px', margin: '4rem auto' }}>
        <p className="eyebrow">Checkout complete</p>
        <h1>Payment successful</h1>

        <p className="hero-text">
          Thank you — your order has been received and your payment was confirmed.
        </p>

        {loadingOrder && (
          <p>Loading order details...</p>
        )}

        {orderError && (
          <p style={{ color: '#fecaca' }}>{orderError}</p>
        )}

        {order && (
          <div style={{ marginTop: '2rem' }}>
            <h2>Order details</h2>

            <p>
              <strong>Order number:</strong> {order.orderNumber}
            </p>

            <p>
              <strong>Total:</strong> {Number(order.totalIncVat).toFixed(0)} SEK
            </p>

            <p>
              <strong>Payment status:</strong>{" "}
              {order.paymentStatus === 1 ? "Paid" : "Processing"}
            </p>

            <h3 style={{ marginTop: '1.5rem' }}>Items</h3>

            {order.items && order.items.length > 0 ? (
              <ul>
                {order.items.map((item) => (
                  <li key={item.id}>
                    <strong>{item.productName}</strong>
                    <div>{item.potSizeLiters}L pot</div>
                    <div>Quantity: {item.quantity}</div>
                    <div>
                      Line total: {Number(item.lineTotalExVat).toFixed(0)} SEK ex VAT
                    </div>
                  </li>
                ))}
              </ul>
            ) : (
              <p>No order items found.</p>
            )}
          </div>
        )}

        {sessionId && (
          <p style={{ wordBreak: 'break-all', opacity: 0.75, marginTop: '2rem' }}>
            Stripe session: {sessionId}
          </p>
        )}

        <Link to="/">
          Back to forest
        </Link>
      </section>
    </main>
  );
}

function ForestApp() {
  const [species, setSpecies] = useState([]);
  const [selectedSpecies, setSelectedSpecies] = useState(null);
  const [viewMode, setViewMode] = useState('forest');
  
  const [cart, setCart] = useState(null);
  

  
  useEffect(() => {
    fetch('https://localhost:7235/api/BambooSpecies')
      .then(res => res.json())
      .then(setSpecies)
      .catch(error => console.error('Error loading bamboo species:', error));
  }, []);

  const [viewportWidth, setViewportWidth] = useState(window.innerWidth);

  useEffect(() => {
    const handleResize = () => setViewportWidth(window.innerWidth);
    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, []);

  const sortedSpecies = useMemo(
    () => [...species].sort((a, b) => (a.heightHigh || 0) - (b.heightHigh || 0)),
    [species]
  );

  
  const renderedSpecies = useMemo(() => {
    if (viewMode === 'compare') {
      return sortedSpecies;
    }

    return [...sortedSpecies].reverse();
  }, [sortedSpecies, viewMode]);
  
  const maxHeight = Math.max(1, ...species.map(s => s.heightHigh || 1));
  const maxDiameter = Math.max(1, ...species.map(s => s.diameterHigh || 1));

  const forestLayout = useMemo(() => {
    return calculateForestLayout(
      renderedSpecies,
      maxHeight,
      maxDiameter,
      viewportWidth
    );
  }, [renderedSpecies, maxHeight, maxDiameter, viewportWidth]);

  const cartItems = cart?.items || [];

  const subtotalExVat = cartItems.reduce(
    (sum, item) => sum + item.unitPriceExVat * item.quantity,
    0
  );

  const vatRate = 0.25; // placeholder – will become dynamic later
  const vat = subtotalExVat * vatRate;

  const getShippingEstimate = (items) => {
    if (items.length === 0) return 0;

    const hasPickupOnly = items.some(
      (item) => item.plantVariant.shippingProfile?.shippingCategory === 4
    );

    if (hasPickupOnly) return 0;

    const totalWeightKg = items.reduce(
      (sum, item) => sum + item.weightKg * item.quantity,
      0
    );

    const maxShippingCategory = Math.max(
      ...items.map((item) => item.plantVariant.shippingProfile?.shippingCategory ?? 0)
    );

    if (maxShippingCategory >= 3 || totalWeightKg > 30) return 349;
    if (maxShippingCategory === 2 || totalWeightKg > 15) return 249;
    if (maxShippingCategory === 1 || totalWeightKg > 5) return 129;

    return 69;
  };

  const shippingEstimate = getShippingEstimate(cartItems);

  const totalIncVat = subtotalExVat + vat + shippingEstimate;

  useEffect(() => {
    const loadCartOnStartup = async () => {
      const response = await fetch(`https://localhost:7235/api/cart/${getSessionId()}`);
      const data = await response.json();
      setCart(data);
    };

    loadCartOnStartup();
  }, []);

  const loadCart = async () => {
    const response = await fetch(`https://localhost:7235/api/cart/${getSessionId()}`);
    const data = await response.json();
    setCart(data);
  };

  return (
    <main className="site-shell">
      <aside className="cart-panel">
        <h2>Cart</h2>

        {cartItems.length === 0 ? (
          <p>No plants added yet.</p>
        ) : (
          <>
            <ul>
              {cartItems.map((item) => (
                <li key={item.id}>
                  <strong>{item.plantVariant.potSizeLiters}L pot</strong>
                  <div>Quantity: {item.quantity}</div>
                  <div>{item.unitPriceExVat} SEK ex VAT</div>
                </li>
              ))}
            </ul>

            <div className="cart-totals">
              <div>Subtotal ex VAT: {subtotalExVat.toFixed(0)} SEK</div>
              <div>VAT ({(vatRate * 100).toFixed(0)}%): {vat.toFixed(0)} SEK</div>
              <div>Shipping estimate: {shippingEstimate.toFixed(0)} SEK</div>
              <strong>Total: {totalIncVat.toFixed(0)} SEK</strong>
            </div>

            <button
              style={{
                marginTop: '1rem',
                padding: '0.75rem 1.25rem',
                background: '#34d399',
                color: '#022c22',
                border: 'none',
                borderRadius: '8px',
                fontWeight: 'bold',
                cursor: 'pointer'
              }}
              onClick={async () => {
                try {
                  const response = await fetch("https://localhost:7235/api/checkout/create-order-from-cart", {
                    method: "POST",
                    headers: {
                      "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                      sessionId: getSessionId()
                    })
                  });

                  if (!response.ok) {
                    alert("Checkout failed");
                    return;
                  }

                  const data = await response.json();

                  if (data.checkoutUrl) {
                    window.location.href = data.checkoutUrl;
                  } else {
                    alert("No checkout URL returned");
                  }
                } catch (err) {
                  console.error("Checkout error:", err);
                  alert("Something went wrong during checkout");
                }
              }}
            >
              Checkout
            </button>

            <button
              style={{
                marginTop: '1rem',
                marginLeft: '1rem',
                padding: '0.75rem 1.25rem',
                background: '#7f1d1d',
                color: '#fef2f2',
                border: 'none',
                borderRadius: '8px',
                fontWeight: 'bold',
                cursor: 'pointer'
              }}
              onClick={async () => {
                try {
                  const response = await fetch("https://localhost:7235/api/cart/clear", {
                    method: "POST",
                    headers: {
                      "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                      sessionId: getSessionId()
                    })
                  });

                  if (!response.ok) {
                    alert("Failed to clear cart");
                    return;
                  }

                  await loadCart();

                } catch (err) {
                  console.error("Clear cart error:", err);
                  alert("Something went wrong clearing the cart");
                }
              }}
            >
              Clear Cart
            </button>
            
          </>
        )}
      </aside>
      <section className={`forest-layout ${viewMode === 'compare' ? 'compare-layout' : 'forest-view'}`}>
        <div className="light-drift" />

        <aside className="hero-box">
          <div className="hero-title">
            <p className="eyebrow">Emeraldine</p>
            <h1>Bambusetum</h1>
          </div>

          <p className="hero-text">
            A living collection of bamboo species cultivated in a southern Nordic climate —
            for structure, material, and quiet presence.
          </p>

          <div className="view-toggle" aria-label="View mode">
            <button
              type="button"
              className={viewMode === 'forest' ? 'active' : ''}
              onClick={() => setViewMode('forest')}
            >
              Forest view
            </button>
            <button
              type="button"
              className={viewMode === 'compare' ? 'active' : ''}
              onClick={() => setViewMode('compare')}
            >
              Compare view
            </button>
          </div>
        </aside>

        <div
          className="culm-field"
          style={{ '--forest-height': `${forestLayout.forestHeight}px` }}
        >
          {viewMode === 'compare' && (
            <div className="compare-hero-spacer" aria-hidden="true" />
          )}

          {renderedSpecies.map((item, index) => {
            const heightRatio = (item.heightHigh || 1) / maxHeight;
            const widthRatio = (item.diameterHigh || 1) / maxDiameter;

            const placement = forestLayout.positions.get(item.id);

            return (
              <article
                className={`species-card species-card-${(index % 18) + 1}`}
                key={item.id}
                style={{
                  '--h': heightRatio,
                  '--w': widthRatio,
                  '--x': placement ? `${placement.x}px` : '0px',
                  '--y': placement ? `${placement.y}px` : '0px'
                }}
              >
                <button
                  className="species-card-frame"
                  type="button"
                  onClick={() => {
                    window.history.pushState({}, '', `/species/${getSpeciesKey(item).replaceAll(' ', '-')}`);
                    setSelectedSpecies(item);
                  }}
                >
                  <img
                    src={getSpeciesImage(item)}
                    className="species-image"
                    alt={getSpeciesKey(item)}
                  />

                  <div className="species-overlay">
                    <p><strong>{getSpeciesKey(item)}</strong></p>
                    <p>{item.heightLow}–{item.heightHigh} m</p>
                    <p>{item.hardinessLow}°C</p>
                    <p>{getPrimaryUse(item)}</p>
                  </div>
                </button>

                <h2 className="species-caption">
                  {item.commonName || item.species}
                </h2>
              </article>
            );
          })}
        </div>
      </section>

      {selectedSpecies && (
        <div className="species-detail-backdrop" onClick={() => setSelectedSpecies(null)}>
          <section className="species-detail" onClick={(event) => event.stopPropagation()}>
            <button
              className="detail-close"
              type="button"
              onClick={() => setSelectedSpecies(null)}
            >
              ×
            </button>

            <img
              className="detail-image"
              src={getSpeciesImage(selectedSpecies)}
              alt={getSpeciesKey(selectedSpecies)}
            />

            <div className="detail-content">
              <p className="detail-eyebrow">{getSpeciesKey(selectedSpecies)}</p>
              <h2>{selectedSpecies.commonName || selectedSpecies.species}</h2>

              <p>{selectedSpecies.notes}</p>

              <dl>
                <div>
                  <dt>Height</dt>
                  <dd>{selectedSpecies.heightLow}–{selectedSpecies.heightHigh} m</dd>
                </div>

                <div>
                  <dt>Diameter</dt>
                  <dd>{selectedSpecies.diameterLow}–{selectedSpecies.diameterHigh} cm</dd>
                </div>

                <div>
                  <dt>Hardiness</dt>
                  <dd>{selectedSpecies.hardinessLow}°C</dd>
                </div>

                <div>
                  <dt>Use</dt>
                  <dd>{getPrimaryUse(selectedSpecies)}</dd>
                </div>
              </dl>
              {selectedSpecies.plantVariants && (
                <div className="variant-section">
                  <h3>Available plant sizes</h3>

                  <ul className="variant-list">
                    {selectedSpecies.plantVariants.map((v) => (
                      <li key={v.id} className="variant-item">
                        <strong>{v.potSizeLiters}L pot</strong>

                        <div>Price: {v.priceExVat} SEK ex VAT</div>
                        <div>Stock: {v.stockQuantity}</div>

                        <div>
                          Height: {v.estimatedPlantHeightLowCm}–{v.estimatedPlantHeightHighCm} cm
                        </div>

                        <div>
                          Shipping: {v.shippingProfile?.canShip ? 'Yes' : 'No'} / Pickup:{' '}
                          {v.shippingProfile?.canPickup ? 'Yes' : 'No'}
                        </div>

                        <div>
                          Category: {v.shippingProfile?.shippingCategory}
                        </div>

                        {v.requiresCulmTrimmingForShipping && (
                          <div>✂ Requires trimming for shipping</div>
                        )}

                        <button
                          type="button"
                          onClick={async () => {
                            await fetch('https://localhost:7235/api/cart/add', {
                              method: 'POST',
                              headers: { 'Content-Type': 'application/json' },
                              body: JSON.stringify({
                                sessionId: getSessionId(),
                                plantVariantId: v.id,
                                quantity: 1
                              })
                            });

                            await loadCart();
                            alert('Added to cart');
                          }}
                        >
                          Add to cart
                        </button>

                      </li>
                    ))}
                  </ul>
                </div>
              )}
            </div>

            {selectedSpecies.technicalProfile && (
              <div className="tech-dump">
                <h3>Full Technical Profile</h3>
                <pre>{JSON.stringify(selectedSpecies.technicalProfile, null, 2)}</pre>
              </div>
            )}
          </section>
        </div>
      )}
    </main>
  );
}

function App() {
  return (
    <Routes>
      <Route path="/" element={<ForestApp />} />
      <Route path="/checkout/success" element={<CheckoutSuccessPage />} />
    </Routes>
  );
}

export default App;