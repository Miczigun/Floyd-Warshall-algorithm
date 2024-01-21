.data
n qword ?
it qword ?

.code
asmFloydWarshall proc
	mov n, rdx
	mov it, rbx

    xor     rbx, rbx
    xor     r8, r8


inner_loop_start:
    ; Compute indices
    mov     r10, it              ; r10 = iteration
    imul    r10, n               ; r10 = iteration * n
    add     r10, r8              ; r10 = i * n + next set if graph vertices is longer than 16
    shl     r10, 1

    mov     r11, rbx                ; r11 = iteration
    imul    r11, n                 ; r11 = iteration * n
    add     r11, it               ; r11 = iteration * n + j
    shl     r11, 1

    mov     r12, rbx               ; r12 = i
    imul    r12, n                 ; r12 = i * n
    add     r12, r8               ; r12 = i * n + j
    shl     r12, 1

    vmovdqu  ymm11, ymmword ptr [rcx + r10] ;Load resultGraph[i * n + iteration]

    vmovdqu  ymm12, ymmword ptr [rcx + r12] ;Load resultGraph[i * n + j]

    mov r13, [rcx + r11]
    movq xmm10, r13

    vpbroadcastw ymm10, xmm10
    vpaddw ymm13, ymm10, ymm11
    vpminuw ymm14, ymm13, ymm12

    
    ; Update resultGraph[i * n + j]
    lea r12, [rcx + r12]
    vmovdqu ymmword ptr [r12], ymm14

    add r8, 16
    cmp r8, n
    je reset_r_eight
    jl inner_loop_start

inner_loop_increment:
    inc     rbx
    cmp     rbx, n
    je      loop_end
    jl      inner_loop_start


loop_end:
	ret

reset_r_eight:
    xor r8, r8
    jmp inner_loop_increment
    

asmFloydWarshall endp
end