import React from "react";
import Footer from "./Footer";
import "../styles/main.css";

export default class Main extends React.Component {
  public render(): JSX.Element {
    return (
      <div id="app" className="container">
        <img
          className="img-thumbnail rounded-circle mt-3"
          src="../../public/cat-paw-logo.png"
        />
        <h1 className="display-3 mt-3">Cat Breeds Detector</h1>
        <hr />
        {/* <NavBar></NavBar> */}
        <Footer></Footer>
      </div>
    );
  }
}
