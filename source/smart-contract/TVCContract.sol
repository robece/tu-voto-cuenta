pragma solidity ^0.4.23;

contract OwnerContract
{
    address internal owner;
    
    modifier onlyOwner() {
        require(isOwner(msg.sender));
        _;
    }

    constructor() public {
        owner = msg.sender;
    }

    function isOwner(address addr) view internal returns(bool) {
        return addr == owner;
    }

    function transferOwnership(address newOwner) internal onlyOwner {
        if (newOwner != address(this)) {
            owner = newOwner;
        }
    }
}

contract TVCContract is OwnerContract
{
    int public total;
    
    struct Record {
        bytes32 username;
        bool registered;
        uint approvals;
        uint disapprovals;
    }
    
    mapping(bytes32 => Record) public Records;
    
    function Register(bytes32 hash, bytes32 username) public onlyOwner {
        //validate if record has been registered before
        require(!Records[hash].registered);
        //update username in record
        Records[hash].username = username;
        //update registered flag
        Records[hash].registered = true;
        //increase records counter
        total++;
	}
	
	function IncreaseApprovals(bytes32 hash) public onlyOwner {
	    //validate if record hasn't been registered before
	    require(Records[hash].registered);
	    //update approval vote counter
	    Records[hash].approvals++;
	}
	
	function IncreaseDisapprovals(bytes32 hash) public onlyOwner {
	    //validate if record hasn't been registered before
	    require(Records[hash].registered);
	    //update disapproval vote counter
	    Records[hash].disapprovals++;
	}
}