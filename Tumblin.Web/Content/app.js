(function () {
    var uploader = {
        controller(attr) {
            var vm = {
                imgUrl: null,
                imgBlob: null,
                title: m.prop(""),
                text: m.prop(""),
                add() {
                    var data = new FormData();
                    data.append("title", this.title());
                    data.append("text", this.text());
                    if (this.imgBlob) {
                        data.append("img", this.imgBlob);
                    }
                    m.request({
                        method: "POST",
                        url: "/api/posts",
                        data: data,
                        serialize(data) {
                            return data;
                        }
                    }).then((post) => {
                        attr.onpostadded(post);
                    });
                    this.title("");
                    this.text("");
                    this.imgUrl = null;
                    this.imgBlob = null;
                },
                handleImage(imageFiles) {
                    if (!imageFiles.length) {
                        return;
                    }
                    console.log(imageFiles);
                    var reader = new FileReader();
                    reader.onload = (e) => {
                        this.imgUrl = e.target.result;
                        this.imgBlob = imageFiles[0];
                        m.redraw();
                    }
                    reader.readAsDataURL(imageFiles[0]);
                }
            };
            return {
                vm
            };
        },
        view(ctrl) {
            var imgUrl = ctrl.vm.imgUrl;
            return m("div", [
                m("input", {
                    value: ctrl.vm.title(),
                    oninput: m.withAttr("value", ctrl.vm.title)
                }),
                m("textarea", {
                    value: ctrl.vm.text(),
                    oninput: m.withAttr("value", ctrl.vm.text)
                }),
                m("div[style=width:100px;height:100px;border:1px solid black]", {
                    ondragenter(e) {
                        e.stopPropagation();
                        e.preventDefault();
                    },
                    ondragover(e) {
                        e.stopPropagation();
                        e.preventDefault();
                    },
                    ondrop(e) {
                        e.stopPropagation();
                        e.preventDefault();
                        var dt = e.dataTransfer;
                        var files = dt.files;
                        ctrl.vm.handleImage(files);
                    }
                }, imgUrl ? m("img[width=100][height=100]", { src: imgUrl }) : "drop"),
                m("div", m("button", {
                    onclick() {
                        ctrl.vm.add();
                    }
                }, "Add"))
            ]);
        }
    };

    var app = {
        controller() {
            var vm = {
                posts: m.prop([]),
                deletePost(post) {
                    m.request({
                        method: "DELETE",
                        url: "/api/posts/" + post.id
                    }).then(() => {
                        _.remove(this.posts(), p => p.id == post.id);
                    });
                }
            };
            m.request({
                method: "GET",
                url: "/api/posts"
            }).then(vm.posts);
            return {
                vm,
                uploader: m.component(uploader, {
                    onpostadded(post) {
                        vm.posts().push(post);
                    }
                })
            };
        },
        view(ctrl) {
            var posts = ctrl.vm.posts();
            return m("main", [
                m("section", ctrl.uploader),
                m("section", posts.map((post) => {
                    var el = m("div[style=width: 300px; min-height: 300px; padding: 5px; float: left;]", [
                        m("h2", m("a", { href: post.id }, post.title)),
                        m("button", {
                            onclick(e) {
                                ctrl.vm.deletePost(post);
                            }
                        }, "delete"),
                        m("p", post.text)
                    ]);
                    if (post.imageId) {
                        el = m("div[style=width: 300px; min-height: 300px; padding: 5px; float: left;]", [
                            m("h2", m("a", { href: post.id }, post.title)),
                            m("button", {
                                onclick(e) {
                                    ctrl.vm.deletePost(post);
                                }
                            }, "delete"),
                            m("img[style=width:100%]", { src: "/api/images/" + post.imageId }),
                            m("p", post.text)
                        ]);
                    }
                    return el;
                })),
            ]);
        }
    };
    function init() {
        m.mount(document.body, m.component(app));
    }
    function main() {
        window.addEventListener("load", init);
    }
    main();
}());