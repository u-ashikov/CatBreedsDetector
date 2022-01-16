import React from "react";
import Footer from "./Footer";
import NavBar from "../navigation/NavBar";
import NavItem from "../navigation/NavItem";
import About from "./About";
import Home from "./Home";
import "../../styles/main.css";

interface ILayoutState {
  displayAbout: boolean;
  displayHome: boolean;
}

export default class Layout extends React.Component<{}, ILayoutState> {
  public constructor(props: any) {
    super(props);

    this.state = {
      displayHome: false,
      displayAbout: false,
    };
  }

  public render(): JSX.Element {
    return (
      <div id="app" className="container">
        <img
          className="img-thumbnail rounded-circle mt-3"
          src="../../public/cat-paw-logo.png"
        />
        <h1 className="display-3 mt-3">Cat Breeds Detector</h1>
        <hr />
        <div className="row">
          <NavBar>
            <NavItem linkText="Home" onClick={() => this.shouldDisplayHome()} />
            <NavItem
              linkText="About"
              onClick={() => this.shouldDisplayAbout()}
            />
          </NavBar>
          <div className="col-9">
            {this.state.displayHome && <Home />}
            {this.state.displayAbout && <About />}
          </div>
        </div>
        <Footer />
      </div>
    );
  }

  private shouldDisplayHome(): void {
    if (this.state.displayHome) return;

    this.setState({
      displayHome: !this.state.displayHome,
      displayAbout: false,
    });
  }

  private shouldDisplayAbout(): void {
    if (this.state.displayAbout) return;

    this.setState({
      displayAbout: !this.state.displayAbout,
      displayHome: false,
    });
  }
}
