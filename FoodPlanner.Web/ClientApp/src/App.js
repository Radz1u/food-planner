import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Receipts } from './components/Receipts';
import { Products } from './components/Products';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/receipts' component={Receipts} />
            <Route path='/products' component={Products} />
      </Layout>
    );
  }
}
