import React from "react";

export default class NavBar extends React.Component {
  public render(): JSX.Element {
    return (
      <div className="col-3">
        <div
          id="v-pills-tab"
          className="nav flex-column nav-pills"
          role="tablist"
          aria-orientation="vertical"
        >
          {this.props.children}
        </div>
      </div>
    );
  }
}
