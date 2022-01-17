import React from "react";

interface IErrorsListProps {
  errors: string[];
}

export default class ErrorsList extends React.Component<IErrorsListProps> {
  public render(): JSX.Element {
    return (
      this.props.errors && (
        <ul>{this.props.errors.map(this.renderSingleError)}</ul>
      )
    );
  }

  private renderSingleError(error: string): JSX.Element {
    return (
      <li v-for="error in errors" v-bind:key="error" className="text-danger">
        <i className="fas fa-times-circle text-danger"></i> {{ error }}
      </li>
    );
  }
}
