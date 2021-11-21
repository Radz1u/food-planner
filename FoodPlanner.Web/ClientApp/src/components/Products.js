import React, { Component } from "react";
import { ProductItem } from "./ProductItem";

export class Products extends Component {
  static displayName = Products.name;
  constructor(props) {
    super(props);
    this.state = { products: [], loading: true };
  }

  async componentDidMount() {
    const response = await fetch("products");
    //  .then((response) => response.json())
    //.then((data) => this.setState({ products: data, loading: false }));

    const data = await response.json();
    this.setState({ products: data, loading: false });
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

    return (
      <div>
        <h1 id="tabelLabel">Products</h1>
        <p>List of products added to database.</p>
        {contents}
      <button className="btn btn-add">+</button>
      </div>
    );
  }
}
