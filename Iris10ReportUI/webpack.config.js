'use strict';
var webpack = require('webpack');
var path = require('path');
var config = {
    devtool: 'source-map',
    context: path.join(__dirname, 'Client'),
    entry: {
        main: './main.ts'
    },
    output: {
        path: path.join(__dirname, 'js'),
        filename: 'main.js'
    },
    module: {
        loaders: [
            {
                test: /\.tsx?$/, loader: 'ts-loader?configFileName=Client/tsconfig.json'
            }

        ],

        preLoaders: [{
            test: /\.js$/, loader: 'source-map-loader'
        }]

     
    },

    plugins: [],
    resolve: {
       
        extensions: ['','.webpack.js', '.web.js', '.ts', '.tsx', '.js']
        
    }
}

module.exports = config;