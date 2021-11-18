import React, { Component } from "react";
import { ProductItem } from "./ProductItem";

export class Products extends Component {
  static displayName = Products.name;
  constructor(props) {
    super(props);
    this.state = { products: [], loading: true };
  }

  componentDidMount() {
    this.populateData();
  }

  static renderTable(products) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th></th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <ProductItem id={product.id} productId={product.id} productName={product.name} />
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      Products.renderTable(this.state.products)
    );

    return (
      <div>
        <h1 id="tabelLabel">Products</h1>
        <p>List of products added to database.</p>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch("products");
    const data = await response.json();
    this.setState({ products: data, loading: false });
  }
}
