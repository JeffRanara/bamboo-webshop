import { useEffect, useMemo, useState } from 'react';
import './App.css';

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

function getForestPlacement(index, widthRatio, heightRatio) {
  const lanes = [
    { x: 91, yStart: 8 },
    { x: 78, yStart: 12 },
    { x: 65, yStart: 4 },
    { x: 52, yStart: 16 },
    { x: 39, yStart: 8 },
    { x: 26, yStart: 20 },
    { x: 20, yStart: 30 },
  ];

  const laneIndex = index % lanes.length;
  const lane = lanes[laneIndex];
  const row = Math.floor(index / lanes.length);

  const rowStep = 26 + heightRatio * 22;

  const xJitter = (index % 5 - 2) * 3.2;
  const yJitter = (index % 4 - 1.5) * 2;

  const widthOffset = widthRatio * 3.5;

  return {
    x: lane.x + xJitter - widthOffset,
    y: lane.yStart + row * rowStep + yJitter
  };
}

function App() {
  const [species, setSpecies] = useState([]);
  const [selectedSpecies, setSelectedSpecies] = useState(null);
  const [viewMode, setViewMode] = useState('forest');

  useEffect(() => {
    fetch('https://localhost:7235/api/BambooSpecies')
      .then(res => res.json())
      .then(setSpecies)
      .catch(error => console.error('Error loading bamboo species:', error));
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

  return (
    <main className="site-shell">
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

        <div className="culm-field">
          {viewMode === 'compare' && (
            <div className="compare-hero-spacer" aria-hidden="true" />
          )}

          {renderedSpecies.map((item, index) => {
            const heightRatio = (item.heightHigh || 1) / maxHeight;
            const widthRatio = (item.diameterHigh || 1) / maxDiameter;

            const pos = getForestPlacement(index, widthRatio, heightRatio);

            return (
              <article
                className={`species-card species-card-${(index % 18) + 1}`}
                key={item.id}
                style={{
                  '--h': heightRatio,
                  '--w': widthRatio,
                  '--x': `${pos.x}%`,
                  '--y': `${pos.y}vh`
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
            </div>
          </section>
        </div>
      )}
    </main>
  );
}

export default App;