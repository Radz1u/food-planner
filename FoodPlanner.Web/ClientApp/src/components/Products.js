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
      showAddModal: false,
      newProductName: "",
    };

    this.showAddProduct = this.showAddProduct.bind(this);
    this.onAddProduct = this.onAddProduct.bind(this);
    this.updateNewProductName = this.updateNewProductName.bind(this);
    this.onCancel = this.onCancel.bind(this);

    this.loadPage = this.loadPage.bind(this);
    this.loadNextPage = this.loadNextPage.bind(this);
    this.loadPreviousPage = this.loadPreviousPage.bind(this);

    this.renderNextPageButton=this.renderNextPageButton.bind(this);
    this.renderPreviousPageButton=this.renderPreviousPageButton.bind(this);
    this.renderAddProductModal=this.renderAddProductModal.bind(this);
    this.renderProductTable=this.renderProductTable.bind(this);
  }

  async componentDidMount() {
    var requestOptions = {
      method: "GET",
      headers: { "Content-Type": "application/json" },
    };

    const response = await fetch("products?take=5");
    const data = await response.json();

    this.setState({
      products: data.items,
      next: data.nextPageToken,
      previous: data.previousPageToken,
      loading: false,
    });
  }

  showAddProduct() {
    this.setState({ showAddModal: true });
  }

  async onAddProduct() {
    var payload = { id: 0, name: this.state.newProductName };
    var requestOptions = {
      method: "POST",
      body: JSON.stringify(payload),
      headers: { "Content-Type": "application/json" },
    };
    var response = await fetch("products", requestOptions);
    let product = await response.json();

    this.setState({
      showAddModal: false,
      newProductName: "",
      products: [...this.state.products, product],
    });
  }

  updateNewProductName(e) {
    this.setState({ newProductName: e.target.value });
  }

  onCancel() {
    this.setState({ showAddModal: false, newProductName: "" });
  }

  async loadPage(token){
    this.setState({loading:true});

    var requestOptions={
      method: "GET",
      headers:{"Content-Type":"application/json"}
    };

    const response = await fetch("products?continuationToken="+token);
    const responseData = await response.json();

    this.setState({
      products: responseData.items,
      next: responseData.nextPageToken,
      previous: responseData.previousPageToken,
      loading: false,
    });
  }

  async loadNextPage(){
    await this.loadPage(this.state.next);
  }

  async loadPreviousPage(){
    await this.loadPage(this.state.previous);
  }

  renderAddProductModal(){
    return this.state.showAddModal ? (
      <Modal isOpen={this.state.showAddModal}>
        <ModalHeader>Add product</ModalHeader>
        <ModalBody>
          <input
            onChange={this.updateNewProductName}
            value={this.state.newProductName}
          />
        </ModalBody>
        <ModalFooter>
          <Button
            className="btn btn-save"
            color="primary"
            onClick={this.onAddProduct}
          >
            Save
          </Button>
          <Button className="btn btn-cancel" onClick={this.onCancel}>
            Cancel
          </Button>
        </ModalFooter>
      </Modal>
    ) : (
      <div></div>
    );
  }

  renderNextPageButton(){
    return this.state.next == null || this.state.next.length == 0 ? (
      <div className="col" />
    ) : (
      <div className="col">
        <button className="btn btn-next" onClick={this.loadNextPage}>&gt;</button>
      </div>
    );
  }

  renderPreviousPageButton(){
    this.state.previous == null || this.state.previous.length == 0 ? (
      <div className="col" />
    ) : (
      <div className="col">
        <button className="btn btn-prev" onClick={this.loadPreviousPage}>&lt;</button>
      </div>
    );
  }

  renderProductTable(){
    this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
          </tr>
        </thead>
        <tbody>
          {this.state.products.map((product) => (
            <ProductItem
              id={product.id}
              productId={product.id}
              productName={product.name}
            />
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    return (
      <div>
        <h1 id="tabelLabel">Products</h1>
        <div className="row container">
          {this.renderProductTable()}
        </div>
        
        <button className="btn btn-add" onClick={this.showAddProduct}>
          +
        </button>
        <div className="row container-navigation">
          {this.renderPreviousPageButton()}
          {this.renderNextPageButton()}
        </div>

        {this.renderAddProductModal()}
      </div>
    );
  }
}
