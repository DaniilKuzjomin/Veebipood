import { useEffect, useRef, useState } from 'react';
import './App.css';

function App() {
  const [tooted, setTooted] = useState([]);
  const idRef = useRef();
  const nameRef = useRef();
  const priceRef = useRef();
  const isActiveRef = useRef();
  const [paymentLink, /*setPaymentLink*/] = useState("");


  useEffect(() => {
    fetch("https://localhost:7061/tooted")
      .then(res => res.json())
      .then(json => setTooted(json));
  }, []);

  function kustuta(index) {
    fetch("https://localhost:7061/tooted/kustuta/" + index, {"method": "DELETE"})
      .then(res => res.json())
      .then(json => setTooted(json));
  }

  ////////////////////////
  function lisa() {
    const uusToode = {
      "id": Number(idRef.current.value),
      "name": nameRef.current.value,
      "price": Number(priceRef.current.value),
      "isActive": isActiveRef.current.checked
    }
    fetch("https://localhost:7061/tooted/lisa", {"method": "POST", "body": JSON.stringify(uusToode)})
      .then(res => res.json())
      .then(json => setTooted(json));
  }
  ////////////////////////

  function dollariteks() {
    const kurss = 1.1;
    fetch("https://localhost:7061/tooted/hind-dollaritesse/" + kurss, {"method": "PATCH"})
      .then(res => res.json())
      .then(json => setTooted(json));
  }

  async function makePayment(sum, index) {
        try {
          const response = await fetch(`https://localhost:7061/Payment/${sum}`);
          if (response.ok) {
            let paymentLink = await response.text();
            
            paymentLink = paymentLink.replace(/^"|"$/g, '');
            console.log('Payment Link:', paymentLink);
            window.open(paymentLink, '_blank');

            kustuta(index);
          } else {
            console.error('Payment failed.');
          }
        } catch (error) {
          console.error('Error making payment:', error);
        }
      }

  return (
    <div className="App">
      <label>ID</label> <br />
      <input ref={idRef} type="number" /> <br />
      <label>Nimi</label> <br />
      <input ref={nameRef} type="text" /> <br />
      <label>Hind</label> <br />
      <input ref={priceRef} type="number" /> <br />
      <label>Aktiivne</label> <br />
      <input ref={isActiveRef} type="checkbox" /> <br />
      <button onClick={() => lisa()}>Lisa</button>
      {tooted.map((toode, index) => 
        <div>
          <div>{toode.id}</div>
          <div>{toode.name}</div>
          <div>{toode.price}</div>
          <button onClick={() => kustuta(index)}>x</button>
          <button onClick={() => makePayment(toode.price)}>Pay</button>
        </div>)}
      <button onClick={() => dollariteks()}>Muuda dollariteks</button>
    </div>
  );
}

export default App;

// import { useRef } from 'react';
// import { useEffect, useState } from 'react';
// import './App.css';

// function App() {
//   const [pakiautomaadid, setPakiautomaadid] = useState([]);
//   const [prices, setPrices] = useState([]);
//   const [chosenCountry, setChosenCountry] = useState("ee");
//   const [start, setStart] = useState("");
//   const [end, setEnd] = useState("");
//   const startRef = useRef();
//   const endRef = useRef();
// const [paymentLink, /*setPaymentLink*/] = useState("");


//   useEffect(() => {
//     if (start !== "" && end !== "") {
//       fetch("https://localhost:7061/nordpool/" + chosenCountry + "/" + start + "/" + end)
//         .then(res => res.json())
//         .then(json => { setPrices(json); })
//     }
//     fetch("https://localhost:7061/parcelmachine")
//        .then(res => res.json())
//        .then(json => setPakiautomaadid(json));
//   }, [chosenCountry, start, end]);

//   function updateStart() {
//     const startIso = new Date(startRef.current.value).toISOString();
//     setStart(startIso);
//   }

//   function updateEnd() {
//     const endIso = new Date(endRef.current.value).toISOString();
//     setEnd(endIso);
//   }

//   async function makePayment(sum) {
//     try {
//       const response = await fetch(`https://localhost:7061/Payment/${sum}`);
//       if (response.ok) {
//         let paymentLink = await response.text();
        
//         paymentLink = paymentLink.replace(/^"|"$/g, '');
//         console.log('Payment Link:', paymentLink);
//         window.open(paymentLink, '_blank');
//       } else {
//         console.error('Payment failed.');
//       }
//     } catch (error) {
//       console.error('Error making payment:', error);
//     }
//   }

  

//   return (
//     <div>
//       <button onClick={() => setChosenCountry("fi")}>Soome</button>
//       <button onClick={() => setChosenCountry("ee")}>Eesti</button>
//       <button onClick={() => setChosenCountry("lv")}>Läti</button>
//       <button onClick={() => setChosenCountry("lt")}>Leedu</button>
//       <input ref={startRef} onChange={updateStart} type="datetime-local" />
//       <input ref={endRef} onChange={updateEnd} type="datetime-local" />
//       {prices.length > 0 && 
//       <table style={{marginLeft: "100px"}}>
//         <thead>
//           <th style={{border: "1px solid #ddd", padding: "12px", backgroundColor: "#04AA6D"}}>Ajatempel</th>
//           <th style={{border: "1px solid #ddd", padding: "12px", backgroundColor: "#04AA6D"}}>Hind</th>
//         </thead>
//         <tbody>
//           <td style={{position: "absolute", left: "30px"}}>{chosenCountry}</td>
//           {prices.map(data => 
//           <tr key={data.timestamp}>
//             <td style={{border: "1px solid #ddd", padding: "8px"}}>{new Date(data.timestamp * 1000).toISOString()}</td>
//             <td style={{border: "1px solid #ddd", padding: "8px"}}>{data.price}</td>
//             <td><button onClick={() => makePayment(data.price)}>Pay</button></td>
//           </tr>)}
//         </tbody>
//       </table>}
//       {paymentLink && (
//         <div>
//           Payment Link: <a href={paymentLink} target="_blank" rel="noopener noreferrer">Pay Now</a>
//         </div>
//       )}
//       <div className="App" style={{border: "1px solid #ddd", padding: "12px", backgroundColor: "#04AA6D"}}>
//         <select>
//           {pakiautomaadid.map(automaat => 
//             <option>
//               {automaat.NAME}
//             </option>)}
//         </select>
//       </div>
//     </div>
//   );
// }

// export default App;