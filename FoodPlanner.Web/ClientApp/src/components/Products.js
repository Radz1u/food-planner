import React, { Component } from "react";
import { ProductItem } from "./ProductItem";
import { Modal, ModalBody, ModalFooter, ModalHeader, Button } from "reactstrap";

export class Products extends Component {
  static displayName = Products.name;
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      loading: true,
      isAdding: false,
      newProductName: "",
    };
    this.showAddProduct = this.showAddProduct.bind(this);
    this.onAddProduct = this.onAddProduct.bind(this);
    this.updateNewProductName = this.updateNewProductName.bind(this);
    this.onCancel = this.onCancel.bind(this);
  }

  async componentDidMount() {
    const response = await fetch("products");
    //  .then((response) => response.json())
    //.then((data) => this.setState({ products: data, loading: false }));

    const data = await response.json();
    this.setState({ products: data, loading: false });
  }

  showAddProduct() {
    this.setState({ isAdding: true });
  }

  async onAddProduct() {
    var payload = { id:0,name: this.state.newProductName };
    var requestOptions = {
      method: "POST",
      body: JSON.stringify(payload),
      headers: { "Content-Type": "application/json" },
    };
    await fetch("products", requestOptions);
    this.setState({ isAdding: false, newProductName: "" });
  }

  updateNewProductName(e){
    this.setState({newProductName:e.target.value});
  }

  onCancel() {
    this.setState({ isAdding: false, newProductName: "" });
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {this.state.products.map((product) => (
            <ProductItem
              refreshCallback={this.componentDidUpdate}
              id={product.id}
              productId={product.id}
              productName={product.name}
            />
          ))}
        </tbody>
      </table>
    );

    let isAdding = this.state.isAdding ? (
      <Modal isOpen={this.state.isAdding}>
        <ModalHeader>Add product</ModalHeader>
        <ModalBody>
          <input 
            onChange={this.updateNewProductName} 
            value={this.state.newProductName} />
        </ModalBody>
        <ModalFooter>
          <Button className="btn btn-save" color="primary" onClick={this.onAddProduct}>Save</Button>
           <Button className="btn btn-cancel" onClick={this.onCancel}>Cancel</Button>
        </ModalFooter>
      </Modal>
    ) : (
      <div></div>
    );

    return (
      <div>
        <h1 id="tabelLabel">Products</h1>
        <p>List of products added to database.</p>
        {contents}
        {isAdding}
        <button className="btn btn-add" onClick={this.showAddProduct}>
          +
        </button>
      </div>
    );
  }
}
