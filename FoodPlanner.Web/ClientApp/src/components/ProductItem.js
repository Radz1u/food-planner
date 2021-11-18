import React, { Component } from "react";

export class ProductItem extends Component {
  constructor(props) {
    super(props);
    this.state = { id: props.productId, name: props.productName };
    this.onDelete = this.onDelete.bind(this);
    this.onEdit = this.onEdit.bind(this);
  }

  componentDidMount() {}

  onEdit() {
    // TODO: Add form for update item before save
    // TODO: Add confirmation message before save(modal window)
    var payload = { id: this.state.id, name: "new val" };
    var requestOptions = {
      method: "PUT",
      body: JSON.stringify(payload),
      headers: { "Content-Type": "application/json" },
    };
    fetch("products", requestOptions).then((response) => response.json);
  }

  onDelete() {
    // TODO: Add confirmation message (modal window)
    fetch("products/" + this.state.id, { method: "DELETE" }).then(
      (response) => response.json
    );
  }

  render() {
    return (
      <tr key={this.state.id}>
        <td>{this.state.name}</td>
        <td>
          <button className="btn btn-edit" onClick={this.onEdit}>
            Edit
          </button>
        </td>
        <td>
          <button className="btn btn-delete" onClick={this.onDelete}>
            Delete
          </button>
        </td>
      </tr>
    );
  }
}
