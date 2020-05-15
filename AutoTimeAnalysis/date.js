function dateToString(o, regex) {
    try {
        if (!o) return '';
        if (typeof o.getMonth === 'function' && !!regex) {
            let splitChar = regex.indexOf('/') > -1 ? '/' : regex.indexOf('-') > -1 ? '-' : regex.indexOf('.') > -1 ? '.' : '';
            let dateSeparate = regex.split(splitChar);
            let result = '';
            for (let item of dateSeparate) {
                let val = '';
                switch (item) {
                    case 'd':
                        val = o.getDate();
                        break;
                    case 'dd':
                        val = this.date2Char(o.getDate());
                        break;
                    case 'M':
                        val = o.getMonth() + 1;
                        break;
                    case 'MM':
                        val = this.date2Char(o.getMonth() + 1);
                        break;
                    case 'yyyy':
                        val = o.getFullYear();
                        break;
                    case 'yy':
                        val = this.date2Char(o.getFullYear());
                        break;
                    default:
                        break;
                }
                result += val + splitChar;
            }
            return result.substring(0, result.length - 1);
        } else {
            return o.toString();
        }
    } catch(ex) { return ''; }
}

function concatDateToString(args) {
    if (!args.length) return '';
    let result = '';
    for (let i = 1; i < args.length; i++) {
        result += args[i] + args[0];
    }
    return result.substring(0, result.length - 1);
}

function date2Char(d){
    return this.rightString('0' + d);
}

function rightString(o) {
    return o.substr(o.length - 2);
}