import React, { Component } from "react";
//TODO:support application errors.
// There is need to handle errors in case of poor connection on client side, or any interruption on server side
export class ProductItem extends Component {
  constructor(props) {
    super(props);
    this.state = {
      id: props.productId,
      name: props.productName,
      visible: true,
      isEditing: false,
      isDeleting: false,
    };

    // method to support delete function
    this.onDelete = this.onDelete.bind(this);
    this.deleteProduct = this.deleteProduct.bind(this);

    // method for support edit function
    this.onEdit = this.onEdit.bind(this);
    this.onNameChange = this.onNameChange.bind(this);
    this.updateProduct = this.updateProduct.bind(this);

    this.onCancel = this.onCancel.bind(this);
  }

  onEdit() {
    this.setState({
      visible: true,
      isEditing: true,
      originName: this.state.name,
    });
  }

  onDelete() {
    this.setState({ visible: true, isDeleting: true });
  }

  async updateProduct() {
    var payload = { id: this.state.id, name: this.state.name };
    var requestOptions = {
      method: "PUT",
      body: JSON.stringify(payload),
      headers: { "Content-Type": "application/json" },
    };
    await fetch("products", requestOptions);
    this.setState({ isEditing: false });
  }

  async deleteProduct() {
    await fetch("products/" + this.state.id, { method: "DELETE" });
    this.setState({ id: "", name: "", visible: false });
  }

  onCancel() {
    if (this.state.isEditing) {
      this.setState({ name: this.state.originName, isEditing: false });
    }

    if (this.state.isDeleting) {
      this.setState({ isDeleting: false });
    }
  }

  render() {
    if (this.state.visible && this.state.isEditing) {
      return this.renderEditable();
    }

    if (this.state.visible && this.state.isDeleting) {
      return this.renderDeleting();
    }

    if (this.state.visible) {
      return this.renderItem();
    } else {
      return null;
    }
  }

  onNameChange(e) {
    this.setState({ name: e.target.value });
  }

  renderItem() {
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

  renderEditable() {
    return (
      <tr key={this.state.id}>
        <td>
          <input
            type="text"
            onChange={this.onNameChange}
            value={this.state.name}
          />
        </td>
        <td>
          <button className="btn btn-save" onClick={this.updateProduct}>
            Save
          </button>
        </td>
        <td>
          <button className="btn btn-cancel" onClick={this.onCancel}>
            Cancel
          </button>
        </td>
      </tr>
    );
  }

  renderDeleting() {
    return (
      <tr key={this.state.id}>
        <td>Do you want to delete {this.state.name} ?</td>
        <td>
          <button className="btn btn-save" onClick={this.deleteProduct}>
            Delete
          </button>
        </td>
        <td>
          <button className="btn btn-cancel" onClick={this.onCancel}>
            Cancel
          </button>
        </td>
      </tr>
    );
  }
}
