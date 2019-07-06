## TuVotoCuenta

TuVotoCuenta is a free and open digital platform developed independently by a group of Mexicans of diverse profiles with the intention of serving as support to protect the integrity of the vote in this and any electoral process. Its main function is to keep inviolable (that is, immutable) a regsitro of the voting totals of each box. To meet this objective, an encryption process is applied to each data collected by citizens and a record is kept in a secure and distributed network called Blockchain. The data can not be modified in any way by anyone (including ourselves) without generating an indelible record that shows that the data was altered; which provides confidence and transparency in the authenticity of the information.

## How does it work?
The user installs the app on their mobile device, and may take pictures of the results blanks in the box in their respective area.

The app automatically saves a copy of the file in a secure database; at the same time, it keeps a cryptographic stamp of the same in a blockchain database, leaving a unique and immutable registry that will guarantee the integrity of the information.

The data is stored in a secure manner and is made available to the public for free access and analysis.

## What is blockchain?
A blockchain is a public data structure that records every transaction executed on a system in the form of linked and encrypted "blocks" to protect the security and privacy of transactions. It can be seen as a notarial record divided into pages. Each page would have an extremely complex notarial seal that would make it impossible to falsify any entry, since the seal would have to be changed as well. In our case, it is also a distributed and secure database that guarantees the integrity and immutability of the data registered in it.

This "chain of blocks" has an important requirement: there must be several users (nodes) that are responsible for verifying each transaction to validate it and thus the block corresponding to that transaction is registered in the chain. The idea is that all these nodes contribute their computing power together to find the most complicated cryptographic seal possible, together with the requirement that they must depend on an extremely sensitive form of each of the entries in the block. As each data block is cryptographically protected, it ensures that hacking or manipulation is almost impossible; and even if some data is breached, there remains an indelible record as a witness, which allows to easily locate manipulated information. The chain name is due to the fact that the next block incorporates the cryptographic stamp of the previous one, making its subsequent manipulation impossible.

Thus, the blockchain or data chain offers a registry to test modifications of each transaction executed in a system, which provides the opportunity to work with data in a reliable and efficient manner.

## How does TuVotoCuenta with Blockchain work?

The platform of TuVotoCuenta keeps a cryptographic stamp of each data raised by the users in a blockchain, thus making it possible to verify that the data is not modified by always having an immutable record associated to each one of them. Even if a hack were given, there would be an indelible record of the alteration, allowing to easily know if the data is reliable or was modified.

TuVotoCuenta calculates a hash value of the photograph and data that is sent from each box. A hash function is extremely sensitive, therefore even changing a pixel of the photo would profoundly alter the result of the function.

This unique identifier is stored along with other numeric data (the total votes, the geographical location of the box) in one of the most stable and reliable blockchains at this moment, the Ethereum platform. We have all the power of the Ethereum nodes to carry out the cryptographic sealing of the blocks, ensuring that nobody, including us, can modify the data once sent to the blockchain.

## Architecture

<div style="text-align:center">
    <img src="https://github.com/robece/tu-voto-cuenta/blob/master/images/tu-voto-cuenta-architecture.png?raw=true" width="800" />
</div>
<br/>